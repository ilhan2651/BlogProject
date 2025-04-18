﻿# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY CoreDemo1.sln ./ 
COPY CoreDemo1/ CoreDemo1/ 
COPY BusinessLayer/ BusinessLayer/ 
COPY DataAccessLayer/ DataAccessLayer/ 
COPY EntityLayer/ EntityLayer/ 
COPY Jwt_Core_Kampı/ Jwt_Core_Kampı/

WORKDIR /app/CoreDemo1
RUN dotnet restore

RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./


# Environment variable for database URL
ENV ASPNETCORE_URLS=http://+:8080
ENV DATABASE_URL=postgresql://blog_project_grvt_user:MXJOzFrKaFCkuR2trf8CegdPT0Ycwm9n@dpg-cume21d2ng1s73823qfg-a.frankfurt-postgres.render.com/blog_project_grvt

# Start the application
CMD ["dotnet", "CoreDemo1.dll"]

EXPOSE 8080
