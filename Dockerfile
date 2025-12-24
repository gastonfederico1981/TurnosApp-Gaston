FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo el contenido
COPY . ./

# Restauramos dependencias buscando el archivo .csproj donde sea que esté
RUN dotnet restore

# Publicamos el proyecto. 
# Si tu carpeta se llama distinto, este comando la encontrará igual.
RUN dotnet publish **/*.csproj -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Iniciamos la aplicación
# Usamos un comando dinámico para que no falle por el nombre de la DLL
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]