using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/Manual/class-ScriptableObject.html

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;

    public float maxTargetForce = 3.0f;
}
