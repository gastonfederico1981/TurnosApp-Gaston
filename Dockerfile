FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copiamos todo
COPY . ./

# 2. Restauramos paquetes
RUN dotnet restore

# 3. Publicamos el proyecto API
RUN dotnet publish **/TurnosApp.API.csproj -c Release -o out

# 4. Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# --- ESTO ES LO QUE FALTA ---
# Informamos a Railway el puerto
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Comando de inicio: Busca específicamente la DLL de la API
ENTRYPOINT ["sh", "-c", "dotnet TurnosApp.API.dll"]