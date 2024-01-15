using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject lookAtObject;
    public float scrollSensitivity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // TODO: trackball: https://www.khronos.org/opengl/wiki/Object_Mouse_Trackball
    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0.0f)
        {
            transform.localPosition += transform.forward * scrollSensitivity;
        }
        if (Input.mouseScrollDelta.y < 0.0f)
        {
            transform.localPosition -= transform.forward * scrollSensitivity;
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
            transform.localPosition += Vector3.right * 1;
        }
        if (Input.GetAxis("Mouse Y") < 0.0f)
        {
            transform.localPosition += Vector3.left * 1;
        }
        if (lookAtObject!=null)
        {
            transform.LookAt(lookAtObject.transform);
        }
    }
}
