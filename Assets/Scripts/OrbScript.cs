/* ORB SCRIPT
 * Responsible for spawning orbs around the map
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public bool orbsActive = true; // Orbs will spawn when this boolean is true
    public Vector3 spawnPoint;
    public int objectCount; 
    public GameObject orb; // GameObject for storing orb prefab
    public float orbSpawnRate = 2.0f; // How many orbs spawn per minute
    public int maxOrbs = 30; // Maximum amount of orbs in scene before orbs are replaced
    public float orbDecayTime; // Destroy timer required to maintain max orbs in scene correctly
    public List<GameObject> orbittingObjects;

    // Start is called before the first frame update
    void Start()
    {
         orbDecayTime = maxOrbs * (60.0f / orbSpawnRate); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        StartCoroutine(OrbSpawningRoutine());
    }

    System.Collections.IEnumerator OrbSpawningRoutine()
    {
        while(true)
        {
            if (orbsActive)
            {
                SpawnOrbs(1);
                Debug.Log("Spawned orb");
                yield return new WaitForSeconds(60.0f / orbSpawnRate);
            }
            yield return null;
        }
    }

    void SpawnOrbs(int amount) 
    {
        float[] xBounds = new float[] {-70, 70};
        float[] yBounds = new float[] {10, 90};
        float[] zBounds = new float[] {-70, 70}; // Upper and lower limits for orb spawning coords

        float x = Random.Range(xBounds[0], xBounds[1]);
        float y = Random.Range(yBounds[0], yBounds[1]);
        float z = Random.Range(zBounds[0], zBounds[1]);
        spawnPoint = new Vector3(x, y, z);

        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedOrb = GameObject.Instantiate<GameObject>(orb, spawnPoint, new Quaternion(0, 0, 0, 0));
            spawnedOrb.transform.localScale = new Vector3(2,2,2);
            Destroy(spawnedOrb, orbDecayTime);
        }
    }
}
