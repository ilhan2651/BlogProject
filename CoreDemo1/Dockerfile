# .NET 8.0 SDK ile build et
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarýný kopyala ve restore et
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Runtime için .NET 8.0 kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Uygulamayý baþlat
CMD ["dotnet", "CoreDemo1.dll"]
