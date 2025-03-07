# Ball_Wars_Unity_Game
This project features a Unity-based game with a **character controller**, **powerup**, and **enemy spawn** system. The player will defeat enemies and collect powerups at specific intervals to gain different abilities.

## Features

- **Character Control**: The player can move, perform actions, send power to enemies, launch projectiles, and jump.
- **Powerups**: Powerups spawn randomly during the game, granting temporary boosts.
  - **Superpower**: Pushes enemies away from the player.
  - **Projectiles**: Allows the player to shoot projectiles at enemies.
  - **Smash**: Enables the player to smash enemies into the air.
- **Enemy and Boss Spawn System**: Enemies spawn in waves, with a boss appearing every third wave.
- **Mini Boss Spawn System**: Mini-bosses spawn periodically based on the player's progress.

## Setup

1. Clone or download the repository.
2. Open the project in Unity.
3. Ensure all scripts are attached to the relevant objects in the scene.
4. Add the necessary prefabs to the scene:
   - **Enemy**: Prefabs for regular enemies.
   - **Boss**: Prefabs for boss enemies.
   - **Powerup**: Prefabs for powerups.
   - **MiniBoss**: Prefabs for mini-bosses.
5. Verify that the `GameManager` and other GameObjects have the scripts assigned.

## How to Play

1. **Movement**: Use the arrow keys or `WASD` keys to move the player.
2. **Powerups**: Collect powerups to gain specific abilities:
   - **Superpower**: Push enemies away.
   - **Projectiles**: Shoot projectiles to hit enemies.
   - **Smash**: Smash enemies into the air.
3. **Enemies**: Enemies will spawn in waves, with bosses and mini-bosses appearing at specific intervals.

## Classes

- **PowerType Enum**: Defines the types of powerups (Superpower, Projectiles, Smash).
- **PlayerController**: Manages player movement and interactions with powerups and enemies.
- **Projectile**: Handles projectiles launched by the player.
- **SpawnManager**: Manages the spawning of enemies and bosses.
- **SpawnMiniBoss**: Spawns mini-bosses at regular intervals.
- **PlayerDeath**: Ends the game when the player falls below a certain height.
- **SpawnPosition**: Generates random spawn positions for enemies and powerups.
