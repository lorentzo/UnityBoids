using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object containing global parameters.
// https://docs.unity3d.com/Manual/class-ScriptableObject.html

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 5.0f;
    public float maxSpeed = 10.0f;

    public float targetWeight = 7.0f;
    public float objectAvoidanceWeight = 10.0f;
    public float boidAvoidanceWeight = 3.0f;
    public float boidGetCloserWeight = 3.0f;

    public float radiusForKeepingDistance = 3.0f;

    public float maxAvoidanceForce = 3.0f;
}
