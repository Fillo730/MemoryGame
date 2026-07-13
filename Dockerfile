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

# Dedicated non-root user: owns only /app and /var/data (SQLite storage), no shell, no home dir.
RUN groupadd --system --gid 1000 appgroup \
 && useradd --system --uid 1000 --gid appgroup --no-create-home --shell /usr/sbin/nologin appuser \
 && mkdir -p /var/data \
 && chown -R appuser:appgroup /app /var/data

COPY --from=build-backend --chown=appuser:appgroup /app/publish .
COPY --from=build-frontend --chown=appuser:appgroup /app/frontend/dist/MemoryGame/browser/. ./wwwroot

USER appuser

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MemoryGameApi.dll"]