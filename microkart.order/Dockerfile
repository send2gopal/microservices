#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microkart.order/microkart.order.csproj", "microkart.order/"]
COPY ["microkart.servicehealth/microkart.shared.csproj", "microkart.shared/"]
RUN dotnet restore "microkart.order/microkart.order.csproj"
COPY . .
WORKDIR "/src/microkart.order"
RUN dotnet build "microkart.order.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "microkart.order.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "microkart.order.dll"]