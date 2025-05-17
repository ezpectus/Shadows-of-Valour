📄 Game Mechanics - Brief Description 


🎮 Player Health and Shield: The player has Health and a Shield. The Shield regenerates after 30 seconds of not taking damage. The Shield blocks a portion of damage; the rest affects Health. Healing: Button: H Uses a Flask, restores ~25% HP. Maximum: 3 Flasks, do not regenerate over time. There is a cooldown between healing uses. HUD: HP / Shield bar. Text display of Flasks.


 👾 Enemies Types: Skeletons - common enemies. Boss - appears only on the final level. Death: Each enemy has an ID. If an enemy with a specific ID has been killed, it does not respawn after loading. Enemies disappear with a delay after death (for animation). Enemy Counter: The GameManager counts all enemies in the level. When the enemy count reaches 0, there is a 2-second delay before transitioning to the next level. 


💾 Saving and Loading What is Saved: Player Health and Position. Current Scene. List of killed enemies (by ID). When Saving Occurs: Automatically every 10 seconds. When ESC is pressed (manual save and exit to menu). Loading: Loads the player, scene, position, and HP. Does not spawn enemies that have already been killed. 


🗺️ Levels and Scenes Scenes: menu - main menu. lvl1, lvl2, lvl3 - game levels. loading_sc - intermediate loading screen. catscene_end - final cutscene. Transitions: Automatic transition to the next level after defeating all enemies (0 enemies). Manual exit to the menu is possible (ESC). 


🔧 Menu Buttons: New Game - resets save data and starts from lvl1. Load - active if a save file exists. Settings - loads the settings scene. Exit - closes the game. ESC in Game: Saves progress. Returns to the menu scene.
 ✅ Example Gameplay Loop: The player enters a level. Fights enemies. Heals when necessary. Defeats all enemies → transitions to the next level. Can exit to the menu, load the game later, and continue from the saved point.