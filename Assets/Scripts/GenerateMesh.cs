/*  
    GenerateMesh.cs

    This script is responsible for the creation of the audio-reactive mesh. It creates a basic
    mesh which covers the entire scene, and when the audio being played gets louder or 
    quieter, the mesh will rise and fall, with some added randomness to make it look better.
    The mesh is updated at a pre-defined interval using a co-routine to save some performace.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    // Necessary variables for creating the mesh
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public bool meshActive = true; // Whether or not to draw the mesh
    public float meshUpdateRate = 10.0f; // How many times a second we want to update the mesh
    private int meshDensity = 20; // The quality of the mesh, aka how many points are in it
    private float meshScale = 7.5f; // The size of the mesh
    private float volume = 0; // Placeholder variable for audio volume
    private AudioManager audioManager; // Placeholder object for retrieving volume

    void Start()
    {
        // Initialising mesh
        mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
        
        // Retrieve volume variable from audio script
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        StartCoroutine(meshCreationRoutine());
    }

    // Generates the vertices (points) which define the mesh structure
    void createVertices(float vol)
    {
        // Create vertex array for mesh points
        int meshVertexCount = (int)Mathf.Pow(meshDensity+1, 2);
        vertices = new Vector3[meshVertexCount];

        // Fill the vertex array with vertices based on scale, density and volume
        for(int index = 0, i = 0; i <= meshDensity; i++)
        {
            for(int j = 0; j <= meshDensity; j++)
            {
                float perlin = Mathf.PerlinNoise(j * 0.2f, i * 0.2f);// Used for adding randomness to the mesh 
                Debug.Log("setting to" + perlin);

                vertices[index] = new Vector3(j * meshScale, perlin * 10.0f, i * meshScale);
                index++;
            }
        }
    }

    // Generates the triangle vertices for the mesh
    void createTriangles()
    {
        // Create triangle array which contains 6 points (2 triangles) per square (4 vertices) in the mesh
        int triangleCount = (int)Mathf.Pow(meshDensity, 2) * 6;
		triangles = new int[triangleCount];

		int quad = 0;
		int triangleIndex = 0;

        // Populate triangle array with appropriate coordinates
		for (int z = 0; z < meshDensity; z++)
		{
			for (int x = 0; x < meshDensity; x++)
			{
                // First triangle
				triangles[triangleIndex + 0] = quad + 0;
				triangles[triangleIndex + 1] = quad + meshDensity + 1;
				triangles[triangleIndex + 2] = quad + 1;
                // Second triangle
				triangles[triangleIndex + 3] = quad + 1;
				triangles[triangleIndex + 4] = quad + meshDensity + 1;
				triangles[triangleIndex + 5] = quad + meshDensity + 2;

				quad++;
				triangleIndex += 6;
			}
			quad++;
		}
    }

    // Creates the mesh given a value for volume
    void CreateMesh(float vol)
    {
        createVertices(vol);
        createTriangles();  
    }   

    // Updates the mesh info
    void UpdateMesh() {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Co routine for creating a new mesh 
    System.Collections.IEnumerator meshCreationRoutine()
    {
        while(true)
        {
            if (meshActive)
            {
                CreateMesh(audioManager.volume);
		        UpdateMesh();    
                yield return new WaitForSeconds(1.0f / meshUpdateRate);
            }
            yield return null;
        }
    }
    
    /* 
    // Draw the mesh points with gizmos
    private void OnDrawGizmos() 
    {
        if(vertices == null)
        {
            return;
        }
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    */
}
