# Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0.302 AS build
WORKDIR /app

# Proje dosyalarýný kopyala ve baðýmlýlýklarý yükle
COPY *.csproj ./
RUN dotnet restore

# Geri kalan dosyalarý kopyala ve yayýn için derle
COPY . ./
RUN dotnet publish -c Release -o /out

# Runtime aþamasý (daha küçük bir imaj kullanýlýr)
FROM mcr.microsoft.com/dotnet/aspnet:8.0.302 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Uygulamayý baþlat
ENTRYPOINT ["dotnet", "CoreDemo1.dll"]
