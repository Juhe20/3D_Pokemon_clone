# Pokémon FireRed/LeafGreen 3D

## Miniproject Pi3D MED3

### Overview of the Game:

The project was created with the old generation 1 of the popular game Pokémon in mind. The game presents the starting town where the player can freely move around. The first route can also be explored when the player has picked up their first Pokémon for their team. On the route random Pokémon spawn and will try to chase down the player to battle them. If the player gets too close they will start an encounter with said Pokémon. The player will have to battle the wild Pokémon with their own new Pokémon until either faints. An NPC has been placed near the start of the route to heal up the player’s team if they end up fainting. 
### The main parts of the game are:

- Player – Trainer, can be moved around with WASD.
- Camera – Following the player and can be moved in 360 degrees around the player.
- Wild Pokémon - Can be collided with by the player to start an encounter. The encounter uses each individual Pokémon’s stats to calculate damage and health.
- NPCs - 2 NPCs that play animations and talk to the player through a dialog window. 1 will talk to the player when receiving their Pokémon the other will heal up the player’s team.
- Map - Made up of roads, trees, houses and ramps. The player can freely move around to explore and can collide with all objects on the map.
- Companion - The player will get their first Pokémon in the lab which is a Pikachu companion that follows the player around and helps the player in battles versus wild Pokémon.


### Game features:

- Random spawns of wild Pokémon that can each be battled
- Dialog panels to help the player know what is going on when interacting in the game.
- Battle system that allows the player to click 4 different buttons that represent different moves to hit the opposing wild Pokémon with.

### **How were the Different Parts of the Course Utilized:**

The game contains a movement script that utilizes affine transformations, as well as a camera that rotates around the player character using the Cinemachine package. Wild Pokémon are spawned in specific areas with a 50% chance when the player is near, which both uses Unity’s random functionality and collisions by using onTriggerEnter to know whether or not the player is within range to spawn them. More usage of the collision system when the player walks into a wild Pokémon or within range of an NPC/interactable object. The player character model is a ragdoll that uses the Unity animator controller to play animations when different conditions are met. All of the non-imported 3D models are made up from primitive objects, mostly cubes and planes with different color materials. The dialog windows and battle UI uses Unity’s UI system with panels and interactable buttons as the GUI.


**How to run it:**
1. Download Unity v. 6000.0.25f1
2. Clone or Download the project
3. Play with mouse and keyboard


### Project Parts:
| Scripts: |  |
| --- | --- |
| BattleController | Controls the turn based battles between the player Pokémon and wild Pokémon. |
| Bush | Contains a list of all bushes in the scene |
| Character Movement | Controls the character and gravity of the character. |
| CheckFlag | Placed on an invisible wall that leads to the first route, blocking the player off until they received their Pokémon. |
| ClearPokemon | Clears all Pokémon that have been instantiated inside a parent Pokémon object that contains all Pokémon that spawns. |
| Door Collision | Handles teleportation of the player between the town and inside the lab where they get their Pokémon. |
| Encounter | Handles behaviour for wild Pokémon and collision between them and the player. |
| HealPlayer | Loops through all team members of the player and heals them to full. Also activates NPC dialog for healing. |
| Move | Handles damage and usage of Pokémon moves |
| PikachuBehaviour | Makes the Pikachu companion follow along the player and teleport if out of range. |
| PlayerTeam | Makes sure the player’s team is alive and also handles adding new party members. |
| PokeballFlagCheck | Checks the whole interaction of receiving the Pokemon and making the NPC in the lab talk with dialog. |
| Pokemon | Contains all stats and movesets of the Pokémon prefabs. |
| PokemonSpawner | Spawns Pokémon randomly on set spawn locations |
| UIController | Handles turning on and off dialog panels and textfields. |



### Models & Prefabs:

- Houses made by Tzuricu - https://sketchfab.com/3d-models/pokemon-hilberts-house-e26227cc4a4645e99b41a32b071228d1
- Player ragdoll XBot by Mixamo - https://www.mixamo.com/#/
- White fence by Vice Cooper - https://poly.pizza/m/4ivPc8ObaIn
- Bookshelf by sirkitree - https://poly.pizza/m/8G2I4M-QVEf
- Pine trees by POLYSCANS - https://www.fab.com/listings/f55a74e4-66c3-4068-9bdf-3987777c58e3
- Flowers by Natalie Rulon - https://poly.pizza/m/bAil3UWF0yp
- Low Poly Water by Ebru Dogan - https://assetstore.unity.com/packages/tools/particles-effects/lowpoly-water-107563
- Pokeball by Daniel Ryan - https://sketchfab.com/3d-models/pokeball-cb1304c3f9064c0287f749aadaa944d6
- Table by Hunter Paramore - https://poly.pizza/m/7qAyGZnerYt
- Pokémon models by poke_master and gaddiellartey2010 - https://sketchfab.com/3d-models/pikachu-bdd57b2bf2374bb89251c083cb2d834e & https://free3d.com/3d-model/pidgey-pokemon-2430.html
- Skybox by BOXOPHOBIC - https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400
- Pathways, grass, ramps and interior in lab were made with Unity primitives.


### Materials:

- Basic Unity materials used for planes, ramps, grass etc. Imported models came with their own materials.
### Scenes:

- Game consists of one scene
### Testing:

- The game was tested on Windows, game cannot be currently played on a mobile platform

## Time Management:

| Task | Time to complete in hours |
| --- | --- |
| Setting up Unity, making a project in GitHub | 0.1 |
| Research and conceptualization of game idea | 0.2 |
| Searching for 3D models | 0.5 |
| Building 3D models from scratch - world building, interior in lab | 2 |
| Making camera movement controls | 1 |
| Player movement | 0.5 |
| Combining player movement with camera orientation | 1 |
| Random Pokémon spawns | 2 |
| Player teleportation scripts - collision detection and new position | 2 |
| Dialog windows and text fields - UI controls | 1 |
| Pokémon team - adding new Pokemons to a list | 1 |
| Player animations - animation controller | 1 |
| Playtesting and bugfixing character controller - gravity  and teleporting | 1.5 |
| Encounters - Triggers, wild Pokémon behaviour and starting encounter | 3 |
| Pokemon battles - Fetching the players team, wild pokemon collided with, moves and battle UI | 10 |
| Event checks and events - Lab visit to receive Pokémon and removal of invisible wall | 1 |
| Heal NPC - healing the party to full | 0.2 |
| Pikachu companion - Behaviour for following the player | 0.5 |
| Making readme | 0.5 |
| All | 29 |
 
### References
- Player animations - https://www.youtube.com/watch?v=vApG8aYD5aI
- Stat script for Pokémon https://www.youtube.com/watch?v=x8B_eXfcj6U&list=PLLf84Zj7U26kfPQ00JVI2nIoozuPkykDX&index=5
- Camera controls - https://www.youtube.com/watch?v=UCwwn2q4Vys
- Sparring in general with ChatGPT but was mostly used for questions regarding the Pokemon battles
- Teleporting player between locations on collision https://www.youtube.com/watch?v=xmhm5jGwonc
