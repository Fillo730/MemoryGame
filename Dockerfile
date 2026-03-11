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
COPY ./MemoryGame_API/MemoryGame_API ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# --- STAGE 3: Runtime Finale ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build-backend /app/publish .
COPY --from=build-frontend /src/frontend/dist/MemoryGame/browser ./wwwroot

# --- CORREZIONE PERMESSI ---
# Creiamo la cartella e assegniamo i permessi all'utente che farà girare l'app
RUN mkdir -p /app/data && chmod 777 /app/data

ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/memorygame.db"
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80
ENTRYPOINT ["dotnet", "MemoryGame_API.dll"]