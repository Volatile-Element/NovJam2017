using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform Target;

    public float SmoothSpeed = 0.125f;

    private Vector3 angularVelocity;

    private void LateUpdate()
    {
        var targetRotation = Quaternion.LookRotation(Target.position - transform.position).eulerAngles;

        Vector3 currentRotation = transform.eulerAngles;

        currentRotation.x = Mathf.SmoothDampAngle(currentRotation.x, targetRotation.x, ref angularVelocity.x, SmoothSpeed);
        currentRotation.y = Mathf.SmoothDampAngle(currentRotation.y, targetRotation.y, ref angularVelocity.y, SmoothSpeed);
        currentRotation.z = Mathf.SmoothDampAngle(currentRotation.z, targetRotation.z, ref angularVelocity.z, SmoothSpeed);

        transform.rotation = Quaternion.Euler(currentRotation);
    }
}