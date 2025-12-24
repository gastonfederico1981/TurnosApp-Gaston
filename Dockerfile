FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia toda la solución para encontrar los proyectos
COPY . ./
RUN dotnet restore TurnosApp.sln

# Publica específicamente el proyecto API
RUN dotnet publish TurnosApp.API/TurnosApp.API.csproj -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Ejecuta la DLL que está dentro de la carpeta out
ENTRYPOINT ["dotnet", "TurnosApp.API.dll"]