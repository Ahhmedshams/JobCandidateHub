#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/JobCandidateHub.API/JobCandidateHub.API.csproj", "src/JobCandidateHub.API/"]
COPY ["src/Core/JobCandidateHub.Application/JobCandidateHub.Application.csproj", "src/Core/JobCandidateHub.Application/"]
COPY ["src/Core/JobCandidateHub.Domain/JobCandidateHub.Domain.csproj", "src/Core/JobCandidateHub.Domain/"]
COPY ["src/External/JobCandidateHub.Infrastructure/JobCandidateHub.Infrastructure.csproj", "src/External/JobCandidateHub.Infrastructure/"]
RUN dotnet restore "./src/JobCandidateHub.API/./JobCandidateHub.API.csproj"
COPY . .
WORKDIR "/src/src/JobCandidateHub.API"
RUN dotnet build "./JobCandidateHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobCandidateHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobCandidateHub.API.dll"]