# 2048 Game - Unity Mobile-Ready Puzzle

A classic 2048 puzzle game built with Unity. This project focuses on clean code architecture, data matrix manipulation, and mobile-ready user interface development.

## 🛠 Tech Stack & Tools
* **Engine:** Unity
* **Language:** C#
* **UI & Animations:** Unity UI (uGUI) & **DOTween** for game timing animations and smooth UI transitions
* **Audio:** Centralized Sound Manager

## 🎯 Implemented Features
* **Core Gameplay Loop:** Full implementation of the classic 2048 logic, including tile movement, generation, and value calculation on a 2D grid.
* **Grid Logic:** Algorithmic calculation of grid states using 2D arrays (matrices) with real-time score tracking.
* **Game Timing:** Implemented timing for the first round along with custom game timing animations powered by **DOTween**.
* **Audio Design:** Integrated a global Sound Manager handling new background music and dedicated sound effects for merging cubes.
* **Visuals & Environment:** Designed a completely new scene, featuring updated 3D materials, a new board layout, and refined aesthetics.
* **Win/Lose Conditions:** Automatic detection of available moves, score goals, and a fully integrated **Game Over panel**.

## 📂 Code Architecture
All core scripts are located in the `Assets/Scripts/` directory. The codebase is strictly decoupled to ensure clear separation of concerns among grid control, tile logic, sound management, and general game states.
