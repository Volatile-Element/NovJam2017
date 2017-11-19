using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 DesiredPosition;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate()
    {
        //var desiredPosition = target.localPosition + OffsetFromTarget;
        //Position
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
        transform.rotation = target.rotation;
        //Look At
        //transform.LookAt(target);
    }
}