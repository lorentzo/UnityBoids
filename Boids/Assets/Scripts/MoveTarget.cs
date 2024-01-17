using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public float movementCircleRadius = 60.0f;
    public float movementCircleRadiusDelta = 1.0f;
    public float theta = 0.0f;
    public float thetaDelta = 0.1f;

    void Awake()
    {
        // Compute 
        // Center of circle is always world (0,0,0).
        /*
        float theta = Mathf.Atan2(transform.position.z, transform.position.x);
        if (transform.position.z > 0 && transform.position.x > 0)
        {
            theta += Mathf.PI;
        }
        */
        float x  = movementCircleRadius * Mathf.Cos(theta);
        float z  = movementCircleRadius * Mathf.Sin(theta);
        float y = 0.0f;
        transform.position = new Vector3(x,y,z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {            
        if (Input.GetKey("a"))
        {
            theta = theta + thetaDelta;
        }
        if (Input.GetKey("d"))
        {
            theta = theta - thetaDelta;
        }
        if (Input.GetKey("w"))
        {
            movementCircleRadius = movementCircleRadius + movementCircleRadiusDelta;
            if (movementCircleRadius > 300.0f) movementCircleRadius = 300.0f;
        }
        if (Input.GetKey("s"))
        {
            movementCircleRadius = movementCircleRadius - movementCircleRadiusDelta;
            if (movementCircleRadius < 0.0f) movementCircleRadius = 0.0f;
        }
        float x  = movementCircleRadius * Mathf.Cos(theta);
        float z  = movementCircleRadius * Mathf.Sin(theta);
        float y = 0.0f;
        transform.position = new Vector3(x,y,z);
    }

    void move_on_circle (Transform transform, float r)
    {
        float x  = r * Mathf.Cos(Time.time + Mathf.PI);
        float z  = r * Mathf.Sin(Time.time + Mathf.PI);
        float y = 0.0f;
        transform.position = new Vector3(x,y,z);
    }
        
}
