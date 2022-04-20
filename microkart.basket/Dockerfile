#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microkart.basket/microkart.basket.csproj", "microkart.basket/"]
COPY ["microkart.servicehealth/microkart.shared.csproj", "microkart.shared/"]
RUN dotnet restore "microkart.basket/microkart.basket.csproj"
COPY . .
WORKDIR "/src/microkart.basket"
RUN dotnet build "microkart.basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microkart.basket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "microkart.basket.dll"]