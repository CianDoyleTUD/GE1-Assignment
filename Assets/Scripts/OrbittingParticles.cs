using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbittingParticles : MonoBehaviour
{
    public ParticleSystem particle;
    public Transform parentOrb;
    public GameObject particle;
    private float spawnRate = 3;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedProjectile = GameObject.Instantiate<GameObject>(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
