using Microsoft.EntityFrameworkCore;
using ProfileService.Middlewares;
using ProfileService.Repository;
using ProfileService.Repository.Interface;
using ProfileService.Service;
using ProfileService.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DislinktDbConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//repositories
builder.Services.AddScoped<IConnectionRequestRepository, ConnectionRequestRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

//services
builder.Services.AddScoped<IConnectionRequestService, ConnectionRequestService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IProfileService, ProfileService.Service.ProfileService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IWorkExperienceService, WorkExperienceService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProfileService", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProfileService v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();

