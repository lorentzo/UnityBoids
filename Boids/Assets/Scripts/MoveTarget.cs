using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    bool moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        moveTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            moveTarget = true;
        }

        if (moveTarget)
        {
            if (Input.GetKey("space"))
            {
                moveTarget = false;
            }

            if (Input.GetAxis("Mouse X") > 0.0f)
            {
                transform.localPosition += Vector3.forward * 1;
            }
            if (Input.GetAxis("Mouse X") < 0.0f)
            {
                transform.localPosition += Vector3.back * 1;
            }
            if (Input.GetAxis("Mouse Y") > 0.0f)
            {
                transform.localPosition += Vector3.up * 1;
            }
            if (Input.GetAxis("Mouse Y") < 0.0f)
            {
                transform.localPosition += Vector3.down * 1;
            }
        }

        
        
    }
}
