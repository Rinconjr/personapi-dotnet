
# Laboratorio 1

Este proyecto es una aplicación ASP.NET Core donde se aplicó el modelo MVC (modelo vista controlador), implementando un CRUD para cada una de las entidades.

## Estructura del Proyecto

El proyecto incluye los siguientes archivos y carpetas clave:

- **personapi-dotnet**: Carpeta principal donde se encuentra la solución de la aplicación.
- **docker-compose.yml**: Archivo que permite construir y ejecutar la aplicación en contenedores Docker.
- **Dockerfile**: Instrucciones para crear la imagen Docker del proyecto.
- **entrypoint.sh**: Script de entrada que automatiza la configuración del contenedor Docker. Es responsable de iniciar SQL Server en segundo plano, esperar a que esté operativo y luego ejecutar el script `init.sql` para inicializar la base de datos.
- **init.sql**: Script de inicialización de la base de datos que configura las tablas necesarias al arrancar el contenedor.

## Configuración del Ambiente

### Requisitos Previos:
- Docker y Docker Compose deben estar instalados en tu máquina.
- Puerto 8080 disponible en tu sistema.

### Pasos para Configurar el Ambiente:

1. Clona este repositorio en tu máquina local:
    ```bash
    git clone https://github.com/Rinconjr/personapi-dotnet.git
    ```
2. Navega a la carpeta del proyecto:
    ```bash
    cd personapi-dotnet
    ```

3. Asegúrate de que Docker esté corriendo en tu sistema.

4. Construye y levanta los contenedores con Docker Compose:
    ```bash
    docker-compose up --build
    ```

5. Verifica que los contenedores estén corriendo correctamente. Puedes comprobar el estado de los contenedores con:
    ```bash
    docker ps
    ```

## Compilación y Despliegue

El despliegue de la aplicación se realiza automáticamente al ejecutar el comando `docker-compose up --build`, que hace lo siguiente:
1. Construye las imágenes Docker para la aplicación y la base de datos.
2. Levanta los contenedores para la aplicación ASP.NET Core y SQL Server.
3. Ejecuta el script `init.sql` para crear las tablas necesarias en la base de datos.

Para acceder a la aplicación, abre tu navegador y dirígete a:

```bash
http://localhost:8080/home
```

o simplemente haz clic [aquí](http://localhost:8080/home) si estás utilizando la máquina local.

