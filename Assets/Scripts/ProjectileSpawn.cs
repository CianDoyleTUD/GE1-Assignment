using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;
    public ParticleSystem particle;
    public List<GameObject> projectileList;
    private float spawnRate = 3;
    private float sphereSize;

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
        Vector3 offset = new Vector3(0.0f, 0.0f, 8.0f);
        GameObject spawnedProjectile = GameObject.Instantiate<GameObject>(projectile, (spawnPoint.transform.position + offset), spawnPoint.transform.rotation);
        spawnedProjectile.GetComponent<Rigidbody>().useGravity = true;
        spawnedProjectile.GetComponent<Rigidbody>().freezeRotation = true;

        sphereSize = Random.Range(1, 4);
        particle.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);
        spawnedProjectile.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize); 
        
        ParticleSystem particleSystem = ParticleSystem.Instantiate<ParticleSystem>(particle, spawnedProjectile.transform.position, spawnedProjectile.transform.rotation);
        particleSystem.transform.parent = spawnedProjectile.transform;
        particleSystem.transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);

        Destroy(spawnedProjectile, 30.0f);
        Destroy(particleSystem, 30.0f);
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
