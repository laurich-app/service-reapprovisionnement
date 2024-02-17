FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ./service-reapprovisionnement/service-reapprovisionnement/service-reapprovisionnement.csproj ./
RUN dotnet restore ./service-reapprovisionnement.csproj
COPY ./service-reapprovisionnement/service-reapprovisionnement .

RUN dotnet build . -c Release -o /app

FROM build as publish
RUN dotnet publish . -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

CMD dotnet service-reapprovisionnement.dll