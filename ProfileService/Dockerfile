FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
ARG github_username
ARG github_pat
WORKDIR /src
COPY ProfileService/ProfileService.csproj ProfileService/
COPY config/NuGet.Config .
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