# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/OGCP.Curriculum.API/OGCP.Curriculum.API.csproj", "src/Presentation/OGCP.Curriculum.API/"]
COPY ["src/Core/Commanding/OGCP.Curriculums.Commanding/OGCP.Curriculums.Commanding.csproj", "src/Core/Commanding/OGCP.Curriculums.Commanding/"]
COPY ["src/Core/ApplicationServices/OGCP.Curriculums.ApplicationServices/OGCP.Curriculums.ApplicationServices.csproj", "src/Core/ApplicationServices/OGCP.Curriculums.ApplicationServices/"]
COPY ["src/Infrastructure/OGCP.Curriculums.AzureServices/OGCP.Curriculums.AzureServices.csproj", "src/Infrastructure/OGCP.Curriculums.AzureServices/"]
COPY ["src/Core/Domain/DomainModel/OGCP.Curriculums.Core.DomainModel/OGCP.Curriculums.Core.DomainModel.csproj", "src/Core/Domain/DomainModel/OGCP.Curriculums.Core.DomainModel/"]
COPY ["src/Infrastructure/Repositories/OGCP.Curriculums.Writes.ProfileRepository/OGCP.Curriculums.Writes.ProfileRepository.csproj", "src/Infrastructure/Repositories/OGCP.Curriculums.Writes.ProfileRepository/"]
COPY ["src/Core/Commanding/OGCP.Curriculums.Querying/OGCP.Curriculums.Querying.csproj", "src/Core/Commanding/OGCP.Curriculums.Querying/"]
COPY ["src/Infrastructure/Repositories/OGCP.Curriculums.Reads.ProfileRepository/OGCP.Curriculums.Reads.ProfileRepository.csproj", "src/Infrastructure/Repositories/OGCP.Curriculums.Reads.ProfileRepository/"]
COPY ["src/Core/ApplicationServices/OGCP.Curriculums.Ports/OGCP.Curriculums.Ports.csproj", "src/Core/ApplicationServices/OGCP.Curriculums.Ports/"]
RUN dotnet restore "./src/Presentation/OGCP.Curriculum.API/OGCP.Curriculum.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/OGCP.Curriculum.API"
RUN dotnet build "./OGCP.Curriculum.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OGCP.Curriculum.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OGCP.Curriculum.API.dll"]