# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./microkart.identity.csproj" --disable-parallel
RUN dotnet publish "./microkart.identity.csproj" -c release -o /app --no-restore

#Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "microkart.identity.dll"]




