# --- STAGE 1: Build Angular ---
FROM node:20 AS build-frontend
WORKDIR /src/frontend
COPY ./MemoryGameAngular/package*.json ./
RUN npm install
COPY ./MemoryGameAngular/ ./
RUN npm run build -- --configuration production

# --- STAGE 2: Build .NET ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-backend
WORKDIR /src/backend

# Modifica qui: copiamo tutto il contenuto della cartella API subito
COPY ./MemoryGame_API/MemoryGame_API ./
# Ora il restore troverà sicuramente il file .csproj
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# --- STAGE 3: Runtime Finale ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build-backend /app/publish .

# Verifica che il percorso dist/MemoryGame/browser sia corretto dopo il build
COPY --from=build-frontend /src/frontend/dist/MemoryGame/browser ./wwwroot

RUN mkdir /app/data
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/memorygame.db"

EXPOSE 80
ENTRYPOINT ["dotnet", "MemoryGame_API.dll"]