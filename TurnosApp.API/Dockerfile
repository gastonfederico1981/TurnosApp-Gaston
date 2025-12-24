FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos y restaurar
COPY *.csproj ./
RUN dotnet restore

# Copiar todo lo demás y publicar
COPY . ./
RUN dotnet publish -c Release -o out

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
# Esto busca cualquier archivo .dll que haya en la carpeta y lo arranca
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]