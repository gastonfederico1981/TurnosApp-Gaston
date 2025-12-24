FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copiamos TODO el contenido al contenedor
COPY . ./

# 2. Buscamos cualquier archivo .csproj y restauramos
RUN dotnet restore

# 3. Publicamos el proyecto buscando el archivo .csproj dinámicamente
RUN dotnet publish **/TurnosApp.API.csproj -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Ejecutamos la DLL (usando el nombre exacto de tu salida)
ENTRYPOINT ["dotnet", "TurnosApp.API.dll"]