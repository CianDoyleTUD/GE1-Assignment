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

It's best explained in two parts:

### The mesh generation
The first part of the project to explain would be the mesh generation which is responsible for the wave like floor which reacts to the audio.
The contents of MeshGenerator.cs is responsible for creating this mesh. According to the tutorial referenced below, to create a mesh you need
a list/array of vertices which represent the points that make up the mesh, which is created in this function:
![image](https://user-images.githubusercontent.com/57116401/146184198-dd5c1463-9164-42a6-b5b7-a2a9a5d67423.png)

And once you have these points, you create an array of triangles, which use these points as reference. For every quad in the mesh,
we create two opposite triangles, which is done in this function:
![image](https://user-images.githubusercontent.com/57116401/146184459-2d3f6adf-ddbc-489f-a2c2-fb9d920ef613.png)

The mesh's y value is also affected by volume, which is a variable created in AudioManager script. The mesh is updated in a coroutine,
which allows us to customise how often the mesh is updated, rather than updating it every frame in Update()
![image](https://user-images.githubusercontent.com/57116401/146184778-6092411e-775a-42da-9257-9987abc7724e.png)

### The audio processing
The audio is attached to an AudioSource component, which we can then use to analyse the audio. The main function in this process is:
![image](https://user-images.githubusercontent.com/57116401/146184993-f5625d0d-fd71-45cd-8dfa-5437e7d7d8f1.png)
Which returns the frequency values for the audio. This is what is used to modify attached GameObjects, and also the volume
variable which is used in MeshGenerator.cs. The audio is broken up into different bands, based on the number of visualiser GameObjects attached.
I chose to use 8 since it works well to represent bass, mids, highs, etc.
Once we have these frequency band values, we are iterating through our visualiser list and performing different operations such as rescaling, 
and adding a particle emitter under a certain conidition, which is also reactive to the properties of the appropriate frequency band:
![image](https://user-images.githubusercontent.com/57116401/146185379-00b4d6cb-1ce5-48c7-8263-5e0acce5e95b.png)
The result is that different orbs will change size as the bass, mids, or lows change, and emit particles like so:
![image](https://user-images.githubusercontent.com/57116401/146185818-1461c2e6-eb78-43c5-b4c1-28eeca4c2e07.png)
![image](https://user-images.githubusercontent.com/57116401/146186067-b954f9c2-1653-4d9a-8bfc-4d725bfaf470.png)


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


