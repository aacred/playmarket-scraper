FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /api
COPY ["InputApi/InputApi.csproj", "InputApi/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "InputApi/InputApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/api/InputApi"
RUN dotnet build "InputApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InputApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InputApi.dll"]