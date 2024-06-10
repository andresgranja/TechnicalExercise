# Use the official Microsoft ASP.NET Core runtime image
# This image includes the runtime and ASP.NET Core libraries
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official Microsoft SDK image to build the project files
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["TechnicalExercise.API/TechnicalExercise.API.csproj", "TechnicalExercise.API/"]
COPY ["TechnicalExercise.Application/TechnicalExercise.Application.csproj", "TechnicalExercise.Application/"]
COPY ["TechnicalExercise.Core/TechnicalExercise.Core.csproj", "TechnicalExercise.Core/"]
COPY ["TechnicalExercise.Infrastructure/TechnicalExercise.Infrastructure.csproj", "TechnicalExercise.Infrastructure/"]

RUN dotnet restore "TechnicalExercise.API/TechnicalExercise.API.csproj"

# Copy everything else and build the application
COPY . .
WORKDIR "/src/TechnicalExercise.API"
RUN dotnet build "TechnicalExercise.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechnicalExercise.API.csproj" -c Release -o /app/publish

# Generate the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechnicalExercise.API.dll"]