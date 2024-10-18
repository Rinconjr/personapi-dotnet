# Laboratorio 1

Este proyecto es una aplicación ASP.NET Core donde se aplico el modelo MVC(modelo vista controlador) donde se implemento un CRUD para cada una de las entidades

## Estructura del Proyecto

El proyecto incluye los siguientes archivos y carpetas clave:

- **personapi-dotnet**: Carpeta principal donde se encuentra la solución de la aplicación.
- **docker-compose.yml**: Archivo que permite construir y ejecutar la aplicación en contenedores Docker.
- **Dockerfile**: Instrucciones para crear la imagen Docker del proyecto.
- **entrypoint.sh**: Script de entrada que automatiza la configuración del contenedor Docker. Es responsable de iniciar SQL Server en segundo plano, esperar a que esté operativo y luego ejecutar el script `init.sql` para inicializar la base de datos
- **init.sql**: Script de inicialización de la base de datos que configura las tablas necesarias al arrancar el contenedor.

## Ejecución con Docker

Para ejecutar la aplicación en contenedores Docker, utiliza el siguiente comando:

```bash
docker-compose up --build
```
