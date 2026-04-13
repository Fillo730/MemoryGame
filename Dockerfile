# --- STEP 1: Build Angular ---
FROM node:22 AS build-frontend
WORKDIR /app/frontend
COPY MemoryGameAngular/package*.json ./
RUN npm install
COPY MemoryGameAngular/ ./
RUN npm run build -- --configuration production

# --- STEP 2: Build .NET ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend
WORKDIR /app/backend
COPY MemoryGameApi/*.csproj ./
RUN dotnet restore
COPY MemoryGameApi/ ./
RUN dotnet publish -c Release -o /app/publish

# --- STEP 3: Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-backend /app/publish .


COPY --from=build-frontend /app/frontend/dist/memory-game/browser ./wwwroot
USER root
RUN chmod -R 777 /app

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MemoryGameApi.dll"]