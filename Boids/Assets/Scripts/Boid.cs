using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    BoidSettings boidSettings;
    GameObject target;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 forward;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 acceleration;

    public void InitializeBoid(BoidSettings boidSettings, GameObject target)
    {
        this.boidSettings = boidSettings;
        this.target = target;

        // Initial position and orientation (forward) was set by BoidSpawn.
        position = transform.position;
        forward = transform.forward;
        velocity = transform.forward * boidSettings.minSpeed;
    }

    public void UpdateBoid()
    {
        Vector3 acceleration = Vector3.zero;

        if (target != null)
        {
            Vector3 targetVector = target.transform.position - position;
            acceleration = Vector3.ClampMagnitude(targetVector.normalized * boidSettings.maxSpeed - velocity, boidSettings.maxTargetForce);
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        transform.position += velocity * boidSettings.minSpeed * Time.deltaTime;
        transform.forward = dir;
        position = transform.position;
        forward = transform.forward;
    }
}
