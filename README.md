# Music visualiser

Name: Cian Doyle

Student Number: C18430304

Class Group: TU856 / DT228

# Description of the project
The aim of this project is to create a virtual environment with visual artifacts which the user can enjoy with as they listen to music. 
The visuals in the world will be affected by the music to really immerse the user into the music being played. 
The user is able to fly around in the space to get a better appreciation  of the visuals. 

# Instructions for use:

To try the project out for yourself, simply clone this repository and open the project in unity.

# How it works

The project can be broken down into two main visualisers
# List of classes/assets in the project and whether made yourself or modified or if its from a source, please give the reference

| Class/asset | Source |
|-----------|-----------|
| MeshGenerator.cs | Adapted from tutorial [here](https://catlikecoding.com/unity/tutorials/procedural-grid/)|
| AudioManager.cs | Mostly self written - Frequency band logic taken from [this video](https://www.youtube.com/watch?v=mHk3ZiKNH48) |
| OrbScript.cs | Self written |
| PlayerMovement.cs | Adapted from class material |
| PlayerCamera.cs | Adapted from class material |
| ProjectileSpawn.cs | Mostly self written - basic coroutine pasted from class material |
| Custom shaders | Created with help of tutorial [here](https://www.codinblack.com/forcefield-shader-using-shader-graph-in-unity3d/) |


# References
I used this tutorial on [generating a mesh from scratch](https://catlikecoding.com/unity/tutorials/procedural-grid/) to understand how to create a mesh,
which I then applied perlin noise to, aswell as adding in my own variable from the audio to get the final result seen here:
![image](https://user-images.githubusercontent.com/57116401/146182877-31a06034-cfb9-43d4-b857-9479537898b6.png)


I used a tutorial on [creating a forcefield shader](https://www.codinblack.com/forcefield-shader-using-shader-graph-in-unity3d/) to understand how to use
the shader graph tool in unity. I then created a custom shader which has a colour variable and a fresnel shader which is mapped to the sine of time. There
are two versions of this shader, one for the center visualier orbs, and one for the wave mesh, the main different being the intensity of the fresnel effect.
![image](https://user-images.githubusercontent.com/57116401/146183004-564f7373-c605-4063-a20a-453be6b3b6c6.png)
![image](https://user-images.githubusercontent.com/57116401/146183067-f4d84fe1-8a7c-4628-b2a9-8d9bde3c572d.png)


I had a look at [this youtube video](https://www.youtube.com/watch?v=mHk3ZiKNH48) to get a sense of how one might break up an audio file into frequency bands
for use in a visualiser. I took the logic of seperating the spectrum data into frequency bands from the video and used it in my code for the center visualiser orbs,
such that one orb represents each band.
This is the code snippet taken from the video:
![image](https://user-images.githubusercontent.com/57116401/146183209-62dd1b7e-113a-4900-ad14-32fe853f72af.png)


# What I am most proud of in the assignment

I am most proud of how the center visualiser orbs turned out, with the flowing shader effect, paired with the particle emitters that appear when 
the music gets more intense. I think it looks quite good and given more time I could make a much more visually appealing project using this concept.


