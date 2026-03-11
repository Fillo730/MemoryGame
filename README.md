# 🧠 Memory Game
An engaging web-based game designed to challenge your memory and track your progress through various levels of difficulty.

---

# 🧐 What is this project about?
I created this game for practice and to give people a fun game to play. The game works by showing a grid of hidden cards; the player must flip them to find matching pairs. As you get better, the grid gets larger and the challenge increases. I built it this way to ensure a smooth experience that saves your best scores and works perfectly on any device.

---

# 🛠 Technologies used

* **TypeScript & Angular**: The "engine" that runs the game in your browser.
* **C# & .NET**: The background system that handles user accounts and scores.
* **SQLite**: A simple file-based system to store all the game data.
* **Docker**: A tool that packages the whole project so it can run anywhere.
* **Render**: The service provider that hosts the game online for everyone to play.

---

# 🎮 How to use the game
* **Open the link**: Visit the [Live Site](https://memorygame-xnn6.onrender.com).
* **Pick a level**: Choose from 9 different difficulties, starting from **"Easy"**.
* **Start Matching**: Click on a card to flip it, then try to find its match.
* **Save your score**: After winning, your game is recorded so you can try to beat them later!

---

# 💻 How to run it on your computer

### 1. Clone the project
Download the code from this repository to your local machine.

### 2. Start with Docker
Open your terminal and run the following commands:

```bash
docker build -t memory-game .
docker run -p 8080:8080 memory-game
