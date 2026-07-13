# 🧠 Memory Game

An engaging web-based memory game with user accounts, saved scores, a global leaderboard, achievements, a friends system, dark/light themes and full multi-language localization.

**[▶ Play it live](https://memorygame-xrxa.onrender.com/)**

![Angular](https://img.shields.io/badge/Angular-20-DD0031?logo=angular&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-database-003B57?logo=sqlite&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-ready-2496ED?logo=docker&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📸 Screenshots

| Home | Gameplay |
|---|---|
| ![Home page](docs/screenshots/home.png) | ![Gameplay with numbered row/column grid](docs/screenshots/gameplay.png) |

| Login | Leaderboard |
|---|---|
| ![Login page](docs/screenshots/login.png) | ![Global leaderboard](docs/screenshots/leaderboard.png) |

---

## 🧐 What is this project about?

I created this game for practice and to give people a fun game to play. The game works by showing a grid of hidden cards; the player must flip them to find matching pairs. As you get better, the grid gets larger and the challenge increases.

Some things it includes beyond the basic game:

* **9 difficulty levels**, from 4 up to 25 pairs of cards.
* A **custom grid layout**: you choose how many columns the board uses, with numbered rows/columns to make it easier to call out card positions to a friend.
* **Accounts & saved scores** — log in to keep track of your best games, or play as a guest.
* A **global leaderboard** — top players, games played per difficulty, and best scores per difficulty.
* A **personal profile** — per-difficulty stats, games-played breakdown charts, game history and achievement progress.
* **Achievements** to unlock as you play.
* A **friends system** — search users, send/accept requests, and check out a friend's public profile.
* **Dark and light themes**, and full **Italian / English / French / German** localization.

---

## 🛠 Technologies used

| Layer | Stack |
|---|---|
| Frontend | **TypeScript & Angular 20** — standalone components, signals, zoneless change detection, `ng2-charts` for data visualization |
| Backend | **C# & .NET 8** — ASP.NET Core Web API, EF Core, JWT authentication |
| Database | **SQLite** — a simple file-based database for all the game data |
| Testing | **xUnit** (backend), **Karma/Jasmine** (frontend) |
| Packaging | **Docker** — multi-stage build packaging frontend + backend into a single image |
| Hosting | **Render** — hosts the live demo |

---

## 🎮 How to use the game

* **Open the link**: visit the [Live Site](https://memorygame-xrxa.onrender.com/).
* **Pick a level**: choose from 9 difficulties, starting from **"Easy"**.
* **Start matching**: click a card to flip it, then try to find its match.
* **Save your score**: log in (or sign up) beforehand and your games are recorded so you can try to beat them later, and appear on the leaderboard!

---

## 💻 How to run it on your computer

### Option 1 — Docker (frontend + backend in one container)

```bash
git clone <this-repo-url>
cd MemoryGame

docker build -t memory-game .
docker run -p 10000:10000 \
  -e JwtSettings__Key="a-long-random-secret-string" \
  -e ConnectionStrings__DefaultConnection="Data Source=MemoryGame.db" \
  memory-game
```

Then open **http://localhost:10000**.

> The two `-e` variables above are required — without them the API fails to start. `JwtSettings__Key` can be any long random string (it signs login tokens); `ConnectionStrings__DefaultConnection` tells the API where to create its SQLite database file.

### Option 2 — Run frontend and backend separately (for development)

**Backend** (`MemoryGameApi/`):
```bash
cd MemoryGameApi
dotnet user-secrets set "JwtSettings:Key" "a-long-random-secret-string"
dotnet run
```
This uses `appsettings.Development.json`, which already points `ConnectionStrings:DefaultConnection` at a local SQLite file. Once running, the Swagger UI is available at **http://localhost:5079/swagger**.

**Frontend** (`MemoryGameAngular/`):
```bash
cd MemoryGameAngular
npm install
npm start
```
Then open **http://localhost:4200**.

---

## ✅ Running the tests

**Backend:**
```bash
cd MemoryGameApi.Tests
dotnet test
```

**Frontend:**
```bash
cd MemoryGameAngular
npm test
```

---

## 📁 Project structure

```
MemoryGame/
├── MemoryGameAngular/     # Angular frontend (game UI, profile, leaderboard, i18n)
├── MemoryGameApi/         # ASP.NET Core Web API (auth, game results, leaderboard, achievements, friends)
├── MemoryGameApi.Tests/   # Backend unit tests (xUnit)
├── docs/                  # Screenshots and documentation assets
├── Dockerfile             # Multi-stage build: Angular + .NET into a single image
└── render.yaml            # Render deployment configuration
```

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

Developed by **Filippo Bratti**.
