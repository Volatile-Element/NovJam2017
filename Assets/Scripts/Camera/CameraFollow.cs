using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

	private void FixedUpdate()
    {
        var desiredPosition = Target.position + Offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(Target);
    }
}