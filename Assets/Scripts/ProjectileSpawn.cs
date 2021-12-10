using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    private float spawnRate = 3;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
	        
    }

    void OnEnable() 
    {
	    StartCoroutine(ProjectileRoutine());
    }
    
    void SpawnProjectile() 
    {
        GameObject spawnedProjectile = GameObject.Instantiate<GameObject>(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Destroy(spawnedProjectile, 5.0f);
    }

    System.Collections.IEnumerator ProjectileRoutine()
    {
        while(true)
        {
            if (Input.GetButton("Fire1"))
            {
                SpawnProjectile();
                yield return new WaitForSeconds(1.0f / spawnRate);
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    	    
    }
}
