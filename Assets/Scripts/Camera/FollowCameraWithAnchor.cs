using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraWithAnchor : MonoBehaviour
{
    public Transform Target;
    public Transform Anchor;
    
    private void FixedUpdate()
    {
        if (Target == null || Anchor == null)
        {
            return;
        }

        //Position
        transform.position = Vector3.Lerp(transform.position, Anchor.position, 0.4f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, 0.4f);

        Vector3 lTargetDir = Target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 4f);
    }

    public void SetInterests(Transform target, Transform anchor)
    {
        if (Target == null)
        {
            transform.rotation = target.rotation;
        }

        if (Anchor == null)
        {
            transform.position = Vector3.Lerp(transform.position, anchor.position, 0.4f);
        }

        Target = target;
        Anchor = anchor;
    }
}