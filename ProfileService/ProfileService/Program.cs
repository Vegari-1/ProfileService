using Microsoft.EntityFrameworkCore;
using ProfileService.Middlewares;
using ProfileService.Repository;
using ProfileService.Repository.Interface;
using ProfileService.Service;
using ProfileService.Service.Interface;
using OpenTracing;
using Jaeger.Reporters;
using Jaeger;
using Jaeger.Senders.Thrift;
using Jaeger.Samplers;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using Prometheus;
using BusService;
using ProfileService.Messaging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// DB_HOST from Docker-Compose or Local if null
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");

// Nats
builder.Services.Configure<MessageBusSettings>(builder.Configuration.GetSection("Nats"));
builder.Services.AddSingleton<IMessageBusSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MessageBusSettings>>().Value);
builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
builder.Services.AddHostedService<ProfileMessageBusService>();

// Postgres
if (dbHost == null)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DislinktDbConnection"),
            x => x.MigrationsHistoryTable("__MigrationsHistory", "profile")));
else
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(dbHost, x => x.MigrationsHistoryTable("__MigrationsHistory", "profile")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Repositories
builder.Services.AddScoped<IConnectionRequestRepository, ConnectionRequestRepository>();
builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

// Services
builder.Services.AddScoped<IConnectionRequestService, ConnectionRequestService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IProfileService, ProfileService.Service.ProfileService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IWorkExperienceService, WorkExperienceService>();

// Sync services
builder.Services.AddScoped<IProfileSyncService, ProfileSyncService>();
builder.Services.AddScoped<IConnectionSyncService, ConnectionSyncService>();
builder.Services.AddScoped<IBlockSyncService, BlockSyncService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProfileService", Version = "v1" });
});

builder.Services.AddOpenTracing();

builder.Services.AddSingleton<ITracer>(sp =>
{
    var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    var reporter = new RemoteReporter.Builder()
                    .WithLoggerFactory(loggerFactory)
                    .WithSender(new UdpSender("host.docker.internal", 6831, 0))
                    .Build();
    var tracer = new Tracer.Builder(serviceName)
        // The constant sampler reports every span.
        .WithSampler(new ConstSampler(true))
        // LoggingReporter prints every reported span to the logging framework.
        .WithLoggerFactory(loggerFactory)
        .WithReporter(reporter)
        .Build();

    GlobalTracer.Register(tracer);

    return tracer;
});

builder.Services.Configure<HttpHandlerDiagnosticOptions>(options =>
        options.OperationNameResolver =
            request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");

var app = builder.Build();

// Run all migrations only on Docker container
if (dbHost != null)
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProfileService v1"));
}

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Prometheus metrics
app.UseMetricServer();

app.Run();

namespace ProfileService
{
    public partial class Program { }
}
