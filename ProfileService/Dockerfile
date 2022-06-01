FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ProfileService/ProfileService.csproj ProfileService/
RUN dotnet restore ProfileService/ProfileService.csproj
COPY . .
WORKDIR /src/ProfileService
RUN dotnet build ProfileService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ProfileService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProfileService.dll"]