FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todo y busca el archivo .csproj en cualquier subcarpeta
COPY . ./
RUN dotnet restore

# Publica el proyecto
RUN dotnet publish -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Este comando arranca el primer .dll que encuentre
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]