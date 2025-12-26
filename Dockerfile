FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copiamos todo el contenido
COPY . ./

# 2. Restauramos paquetes ignorando errores de estructura
RUN dotnet restore

# 3. Publicamos buscando el archivo que acabamos de crear
RUN dotnet publish **/TurnosApp.API.csproj -c Release -o out

# 4. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# 5. Comando de arranque flexible
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]