FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /api
COPY ["GrpcApplication/GrpcApplication.csproj", "GrpcApplication/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "docker /GrpcApplication.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/api/GrpcApplication"
RUN dotnet build "GrpcApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcApplication.dll"]