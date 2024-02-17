FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0.2 as build
