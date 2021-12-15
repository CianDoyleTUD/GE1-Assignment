using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    int meshDensity = 20;
    private float meshScale = 7.5f;

    void Start()
    {
        mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		CreateMesh();
		UpdateMesh();
    }

    void createVertices()
    {
        // Create vertex array for mesh points
        int meshVertexCount = (int)Mathf.Pow(meshDensity+1, 2);
        vertices = new Vector3[meshVertexCount];

        // Fill the vertex array with vertices based on scale and density
        for(int index = 0, i = 0; i <= meshDensity; i++)
        {
            for(int j = 0; j <= meshDensity; j++)
            {
                vertices[index] = new Vector3(j * meshScale, 0, i * meshScale);
                index++;
            }
        }
    }

    void createTriangles()
    {
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

    void CreateMesh()
    {
        createVertices();
        createTriangles();  
    }

    void UpdateMesh() {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    
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
}
