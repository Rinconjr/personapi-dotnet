# Usa la imagen oficial de .NET 8 para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY personapi-dotnet/*.csproj ./personapi-dotnet/
WORKDIR /app/personapi-dotnet
RUN dotnet restore

# Vuelve al directorio principal y copia todo el contenido del proyecto
COPY . .

# Publica la aplicación en un directorio "out"
RUN dotnet publish -c Release -o out

# Usa una imagen más ligera de .NET 8 para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/personapi-dotnet/out .

# Expone el puerto en el que la aplicación correrá (ajustar si es necesario)
EXPOSE 5293

# Define el comando que se ejecutará cuando el contenedor inicie
ENTRYPOINT ["dotnet", "personapi-dotnet.dll"]
