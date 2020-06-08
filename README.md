# OculusVR_Demo

By Steven Feng 2020/04/24

In this project, we are building an oculus go game based on the arcade game Asteroid.

To modify this project:   
Keystore alias: keystore1   
Keystore password: A1b2c3d4   
Go to Unity, File> Build Setting> Player Setting > Publish Setting    
Under Project Keystore, enter password   

## how it works:
Object will spawn infront of the player, around 40 - 50 m from the player and heading towards the player's general direction at a random speed (1 - 5 m/s) The player have 3 types of weapons to break the object before it hits the player. 

### weapons
The missle, which explodes on impact, can break (for whole objects) or destroy (for fragmented objects) the game objects within a small radius. 
The pointer, which has a 10 m range, can break (or destroy) objects that it touches, when the player tap on the touch pad
The super weapon, which has a fuse and will destroy or break objects within 30 meters. 
Player will start with 1 super weapon, and 5 lives. 

### score
10 points are awarded for every object broke or destroyed. For every 1000 points, a super weapon is awarded to the player's arsenal

### life
The player has a total of 5 lives to start with, and will lose a life everytime a spawned object get within 0.1 m of the player. We may add a feature to reward the player with more lives when they hit the med kit.

### Gravity Mode
In this mode, fragmented objects, and player's projectile (a hammer) will both be effected by gravity. Instead of asteroids, western themed props will be spawned instead. The hammer has a short fuse and will break near by objects on impact. Also, for this mod, objects will be spawned at a closer range to the player due to the limited range of the hammer.

### Fow how this game was made, and outline for scripts used in this project, refer to the following evernotes:
Development Set Up
https://www.evernote.com/shard/s507/sh/51b8cb2d-138f-487f-9d5d-9d893453860e/4b4949b9fc724a90f1778da4d6af48ed 

Oculus Go Sideloading APP
https://www.evernote.com/shard/s507/sh/607f6d33-738b-429c-a524-8f04d62c8be0/f623558c32329bb556e50892af055418 

Asteroid Remastered
https://www.evernote.com/shard/s507/sh/5a3126fe-c8fb-4698-a004-21a4831ad3b0/a21c977d5de393ea241e40d63b8c7dd7 

Uploading to Oculus Store
https://www.evernote.com/shard/s507/sh/d4d234f8-cb54-72ba-bc20-7ba3e9272b49/398827ea3cadf9cf44cecb899fc01a91

## Set Up Dev Environment 
Download Unity and get the assets below, either from Unity Asset store or online, and drag into the asset folder in the directory

Assets used:
VR Scene N02, used as ground plane for gravity mode:
https://assetstore.unity.com/packages/3d/environments/vr-scenes-2-141183

Oculus Integration, for interfacing with Oculus Go
https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022

Skybox Volume 2, for back ground skybox
https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-volume-2-nebula-3392

Unity Particle Pack, for explosion effects
https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325

Western Prop pack, for destructible objects:
https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325

Rockets, Missles, and Bombs - Cartoon Low Poly Pack, for missle and super weapon prefabs
https://assetstore.unity.com/packages/3d/props/weapons/rockets-missiles-bombs-cartoon-low-poly-pack-73141


## Resources:
Shatter, Destruction, Bomb effects in Unity:
https://www.youtube.com/watch?v=EgNV0PWVaS8
https://www.youtube.com/watch?v=BYL6JtUdEY0

Spawning object:
Instantiate: https://www.youtube.com/watch?v=BYL6JtUdEY0
Object Pool: https://www.youtube.com/watch?v=tdSmKaJvCoA

VR - oculus:
Simple pointer: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
Menu: https://www.youtube.com/watch?v=__iTtJHZg6k&t=127s

C# Unity Script:
Scrip-table object: https://www.youtube.com/watch?v=aPXvoWVabPY&t=312s
Mono Behavior Object: https://www.youtube.com/watch?v=E-qJzEpTVA4&list=PLdE8ESr9Th_tLSOvCRbDyfhLAqVH7omOg


For Oculus Go Development, check out
https://www.evernote.com/shard/s507/sh/51b8cb2d-138f-487f-9d5d-9d893453860e/4b4949b9fc724a90f1778da4d6af48ed

For sideloading APP on Oculus Go (if you are developing this on Unity, Unity will load it for you automatically)
https://www.evernote.com/shard/s507/sh/607f6d33-738b-429c-a524-8f04d62c8be0/f623558c32329bb556e50892af055418
