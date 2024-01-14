using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManage : MonoBehaviour
{
    public BoidSettings boidSettings;
    public GameObject target;
    Boid[] boids;
    bool moveToTarget;

    // Start is called before the first frame update
    void Start()
    {
        moveToTarget = false;
        boids = FindObjectsOfType<Boid>(); // Boids can be created from multiple sources (https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html).
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].InitializeBoid(boidSettings, target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space"))
        {
            moveToTarget = true;
        }
        if (moveToTarget)
        {
            if (Input.GetKeyDown("space"))
            {
                moveToTarget = false;
            }
        }

        // Compute average center of boid system.
        Vector3 avgBoidCenter = Vector3.zero;
        for (int i = 0; i < boids.Length; i++)
        {
            avgBoidCenter += boids[i].transform.position;
        }
        avgBoidCenter /= boids.Length;

        // Update boids.
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].UpdateBoid(avgBoidCenter, false);
        }
    }
}
