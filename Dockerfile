FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo (Core, Infra y API)
COPY . ./

# Restauramos todos los proyectos a la vez
RUN dotnet restore

# Publicamos la API (Asegurate que el nombre coincida con el archivo que encuentres)
RUN dotnet publish **/TurnosApp.API.csproj -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Arrancamos la API
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]