using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManage : MonoBehaviour
{
    public BoidSettings boidSettings;
    public GameObject target;
    Boid[] boids;

    // Start is called before the first frame update
    void Start()
    {
        boids = FindObjectsOfType<Boid>(); // Boids can be created from multiple sources.
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].InitializeBoid(boidSettings, target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].UpdateBoid();
        }
    }
}
