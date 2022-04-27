#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microkart.catalog/microkart.catalog.csproj", "microkart.catalog/"]
COPY ["microkart.servicehealth/microkart.shared.csproj", "microkart.shared/"]
RUN dotnet restore "microkart.catalog/microkart.catalog.csproj"
COPY . .
WORKDIR "/src/microkart.catalog"
RUN dotnet build "microkart.catalog.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "microkart.catalog.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "microkart.catalog.dll"]