# 1️⃣ .NET 8 SDK kullanarak build işlemi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2️⃣ Çözüm dosyasını ve projeleri eksiksiz kopyala
COPY CoreDemo1.sln ./ 
COPY CoreDemo1/ CoreDemo1/ 
COPY BusinessLayer/ BusinessLayer/ 
COPY DataAccessLayer/ DataAccessLayer/ 
COPY EntityLayer/ EntityLayer/ 
COPY Jwt_Core_Kampı/ Jwt_Core_Kampı/

# 3️⃣ Önce bağımlılıkları yükle (cache kullanarak hızlı build)
WORKDIR /app/CoreDemo1
RUN dotnet restore

# 4️⃣ Projeyi build et ve publish et
RUN dotnet publish -c Release -o /out

# 5️⃣ .NET 8 Runtime kullanarak final image oluştur
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# 6️⃣ Render ve Docker için ortam değişkenlerini ayarla
ENV ASPNETCORE_URLS=http://+:8080
ENV DATABASE_URL=${DATABASE_URL}  # Render'dan gelen PostgreSQL bağlantısı

# 7️⃣ Uygulamayı başlat
CMD ["dotnet", "CoreDemo1.dll"]

# 8️⃣ Port ayarı (Render ve Docker için)
EXPOSE 8080
