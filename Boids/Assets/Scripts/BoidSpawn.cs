using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawn : MonoBehaviour
{
    public Boid boidPrefab;
    public int nBoids;
    public float boidSpawnRadius;

    void Awake()
    {
        for (int i = 0; i < nBoids; i++)
        {
            Boid boid = Instantiate(boidPrefab);
            boid.transform.position = transform.position + Random.insideUnitSphere * boidSpawnRadius;
            boid.transform.forward = Random.insideUnitSphere;
        }
    }
}
