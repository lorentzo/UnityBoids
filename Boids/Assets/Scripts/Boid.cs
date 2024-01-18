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
        velocity = transform.forward * Random.Range(boidSettings.minSpeed, boidSettings.maxSpeed);
    }

    public void UpdateBoid(Vector3 avgBoidCenter, bool moveToTarget)
    {
        // Accumulating vector which takes in account rules of boid system.
        // Used for computing velocity and thus position vectors.
        Vector3 acceleration = Vector3.zero;

        // Enable moving boids toward target.
        if (target != null && moveToTarget)
        {
            Vector3 targetVector = target.transform.position - position;
            acceleration += Vector3.ClampMagnitude(targetVector.normalized * boidSettings.maxSpeed - velocity, boidSettings.maxAvoidanceForce) * boidSettings.targetWeight;
        }

        // Evade collisions with other objects but not other boids!
        // TODO: testing with rays from forward to out.
        RaycastHit hitContext;
        if(isObstacleDirection(transform.position, transform.forward, out hitContext))
        {
            Vector3 nonObstacleDirection = computeNonObstacleDirection();
            acceleration += Vector3.ClampMagnitude(nonObstacleDirection * boidSettings.maxSpeed - velocity, boidSettings.maxAvoidanceForce) * boidSettings.objectAvoidanceWeight;
        }

        // Keep distance.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, boidSettings.radiusForKeepingDistance);
        Vector3 clearDirection = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 moveDirection = transform.position - hitColliders[i].transform.position;
            clearDirection += moveDirection.normalized;
        }
        acceleration += Vector3.ClampMagnitude(clearDirection * boidSettings.maxSpeed - velocity, boidSettings.maxAvoidanceForce) * boidSettings.boidAvoidanceWeight;

        // Don't go too far from boid crowd.
        Vector3 getCloserDirection = avgBoidCenter - transform.position;
        acceleration += Vector3.ClampMagnitude(getCloserDirection.normalized * boidSettings.maxSpeed - velocity, boidSettings.maxAvoidanceForce) * boidSettings.boidGetCloserWeight;

        // Compute velocity, position and orientation from accumulated acceleration.
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, boidSettings.minSpeed, boidSettings.maxSpeed);

        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;
        position = transform.position;
        forward = transform.forward;
    }

    bool isObstacleDirection(Vector3 position, Vector3 direction, out RaycastHit hitContext)
    {
        // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        return Physics.Raycast(
            position, 
            direction, 
            out hitContext,
            Mathf.Infinity, 
            Physics.DefaultRaycastLayers, 
            QueryTriggerInteraction.UseGlobal); 
    }

    Vector3 computeNonObstacleDirection()
    {
        int nTestPoints = 500;
        Vector3 nonObstacleDirection = transform.forward;
        RaycastHit hitContext;
        for (int i = 0; i < nTestPoints; i++)
        {
            Vector3 potentialDirection;
            while (true)
            {
                potentialDirection = Random.onUnitSphere;
                if (Mathf.Acos(Vector3.Dot(potentialDirection,transform.forward)) < 0.3f)
                {
                    break;
                }
            }

            if (!isObstacleDirection(transform.position, potentialDirection.normalized, out hitContext))
            {
                nonObstacleDirection = potentialDirection.normalized;
                //Debug.DrawRay(transform.position, potentialDirection.normalized * hitContext.distance, Color.green);
                //Gizmos.DrawRay(transform.position, potentialDirection.normalized * hitContext.distance);
                break;
            }
            else
            {
                //Gizmos.DrawRay(transform.position, potentialDirection.normalized * hitContext.distance);
                //Debug.DrawRay(transform.position, potentialDirection.normalized * hitContext.distance, Color.red);
            }
        }
        return nonObstacleDirection;
    }

    Vector3 SamplePointOnUnitSphere()
    {
        // http://corysimon.github.io/articles/uniformdistn-on-sphere/
        float theta = 2 * Mathf.PI * Random.value;
        float phi = Mathf.Acos(1 - 2 * Random.value);
        float x = Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = Mathf.Sin(phi) * Mathf.Sin(theta);
        float z = Mathf.Cos(phi);
        return new Vector3(x, y, z);
    }
}
