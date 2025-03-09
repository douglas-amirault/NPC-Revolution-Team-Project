Start Scene File: ???

How to Play: 

Navigate your way to the exit door 
Use the arrow/wasd keys to move. Use the space key to jump. 

**Level 1**
As in other levels, the overarching goal is to navigate past enemy AIs (the Roombas)
and obstacles (including the killer wall prefabs). 


When the game starts the game object Roomba (green top AI) and ChasingRoomba (red top AI)
are facing the player. Note that the Roomba game objects are nav mesh agents that use 
waypoints to patrol and break their patrol when Player is within a certain distance. 
Prior to breaking patrol an animation is shown where the Roomba appears to be aggravated;
if the Player gets closer it will attack. The ChasingRoomba game objects dart at the player
when Player is in range via an is trigger collider. 


A game dynamic to note is that the Player can jump on and deactivate the Roomba 
game object.


A major obstacle in this level is the killer wall prefab. It has a trigger 
collider on it that plays a mechanical wall sound when triggered by Player.
The AudioSource component is added dynamically via Assets\Scripts\KillerWallAudio.cs
to reduce computational complexity. Each wall prefab includes a child knife game 
object that is comprised of a handle and blade made from primitive Unity objects.
The blade has a script attached: Assets\Scripts\KnifeTrigger.cs. This script 
dynamically adds the AudioSource component (to save resources since there are
many walls on this level). The script also ends the game if Player collides 
with its is Trigger collider. (A separate collider is used to ensure objects
do not pass through the wall). The wall has a rigid body with is kinematic set 
to true to support the animation that moves it. 


In terms of design, the right side of the map appears to offer the best path to 
the door that wins the level. However, there are numerous enemy AI on that path.
Moreover, the user can see the door object through a glass wall. This glass wall
blocks the door and tempts the player to go right on the more dangerous path.
If the player moves that way and enters the glass wall corridor, they are met 
with numerous patrolling Roombas. An alternative path is to use the moving 
cabinets to the left of the glass wall.


These cabinets are generally an obstacle, but in this level they are meant to be
leveraged by the player. They move left and right via Assets\Scripts\FileCabinetMover.cs.
The player should notice when one of them is more to the right so they can jump
on the cabinet and then jump over the glass wall. 

Once over the glass wall, the player presses "f" at the button next to the door.
It opens and they win the level. The button script is here: Assets\Scripts\ButtonPressInteraction.cs.
That script also ensures seamless transitions from one level to the next. 




Known Bugs: 


Team Contributions:

- Douglas Amirault

Douglas created the team's Git repository and the level prefabs (four walls, floor, final door and buttons, with open
and pressing animations with an f key press, respectively) for small and large levels. He also wrote the script to
handle level transitions (Assets/Scripts/ButtonPressInteraction.cs), as well as selecting and adding three
different audio files from the asset store to play in each of the three levels we have implemented. Finally, he
designed and implemented the Level 3.unity scene.


External Assets:

Assets/Audio/Music/Tristan_Nolan/Nox - Dark & Futuristic Music Pack
    • https://assetstore.unity.com/packages/audio/music/electronic/nox-dark-futuristic-music-pack-241162

- Pedro Garboza-Hong

Pedro implemented the player movement and camera controls. This includes all scripts in Assets/Scripts/MovementStates

External Resources:

Third Person Shooter (Unity Tutorial) Ep 1 Movement and Camera
    • https://www.youtube.com/watch?v=KCYr5pFC6Sw&list=PLX_yguE0Oa8QmfmFiMM9_heLBeSA6sNKx

Third Person Shooter (Unity Tutorial) Ep 2 Animation Blend Trees
    • https://www.youtube.com/watch?v=IhEgT4yMLMA&list=PLX_yguE0Oa8QmfmFiMM9_heLBeSA6sNKx&index=4

Third Person Shooter (Unity Tutorial) Ep 3 Finite State Machines
    • https://www.youtube.com/watch?v=bH6ueZpF58A&list=PLX_yguE0Oa8QmfmFiMM9_heLBeSA6sNKx&index=5

Third Person Shooter (Unity Tutorial) Ep 12 Jumping
    • https://www.youtube.com/watch?v=qRY0d5QEGnc&list=PLX_yguE0Oa8QmfmFiMM9_heLBeSA6sNKx&index=16

External Assets:

Assets/Advaita Assets/Low Poly Prototype Character/*:
    • https://assetstore.unity.com/packages/3d/characters/humanoids/low-poly-prototype-character-294200?srsltid=AfmBOoovUH4d3n4OEeA63xlLxu5gFKKRAYNFACkGLPzCp8-asNbs4y76

Assets/Blink/Art/Animations/Animations_Starter_Pack:
    • https://assetstore.unity.com/packages/3d/animations/free-32-rpg-animations-215058

- Derek Griffing

Derek created the "killer wall" prefab MovingWallLeftRight from primitive Unity game objects.
This included setting up dynamic instantiation of an AudioSource object to a play a mechanical 
wall sound when the player is near, and adding a script to the blade on each wall's knife that
ends the game and plays a stabbing sound. It also involved developing the moving wall animation
and setting it up in Animator. He identified the external audio files and edited them.
He also designed and implemented the level1.unity scene.

**Scripts**
 - Wrote: Assets\Scripts\KnifeTrigger.cs, Assets\Scripts\KillerWallAudio.cs
 - Added audio handling to Assets\Scripts\ProximityActivator.cs, 
   Assets\Scripts\ButtonPressInteraction.cs (level end)
 - Edited Assets\Scripts\Roomba.cs to handle patrolling issues
 - Edited Assets\Scripts\ChasingRoomba.cs to speed charging and attack, and ensure
   Roomba AI enemies target player.


External assets: 

Audio/SFX/knife_sound.mp3 [Trimmed]
    • https://pixabay.com/sound-effects/search/stab/

Audio/SFX/door_level_end_sound.mp3
    • https://pixabay.com/sound-effects/search/spaceship-doors/

Audio/SFX/killer_wall_sound.mp3
    • https://pixabay.com/sound-effects/search/mechanical/

Assets/SciFiOfficeLite
    • https://assetstore.unity.com/packages/3d/environments/sci-fi/free-sci-fi-office-pack-195067

- Faith Madeoy Gault

Faith designed and implemented the roombas enemies. This includes putting together the model from Unity primitive objects, 
the ChasingRoomba.cs, JumpingRoomba.cs, and Roomba.cs scripts, creating custom animations in Animations/EnemyAnimations, 
and combining these elements to make the three roomba prefabs objects in Prefabs. She also designed layout of the level2.unity scene.
Additional supplental assets created included Prefab/ClimbableWall.prefab and Prefab/waypoint.prefab. 

No external assets were used.

Fanny Martinez Contributions

-Designed and implemented the brown file cabinet prefab (Assets/Prefab/FileCabinet)
  **Developed the associated script: Assets/Scripts/FileCabinetMover.cs
-Designed and implemented the Start Menu
  **Created the related scripts: Assets/Scripts/Menu.cs and Assets/Scripts/GameQuitter.cs
-Designed and implemented the In-Game to Level 1 transition system
  **Developed the associated script: Assets/Scripts/InGameMenu.cs

