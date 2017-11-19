using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    const float G = 667.4f;
    public static List<Gravity> GravityObjects = new List<Gravity>();

    public bool GravityDisabled;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GravityDisabled)
        {
            return;
        }

        foreach (var gravity in GravityObjects)
        {
            if (gravity == this || gravity.GravityDisabled)
            {
                continue;
            }

            Attract(gravity);
        }
    }

    private void OnEnable()
    {
        if (GravityObjects == null)
        {
            GravityObjects = new List<Gravity>();
        }

        GravityObjects.Add(this);
    }

    private void OnDisable()
    {
        GravityObjects.Remove(this);
    }

	public void Attract(Gravity attractingObject)
    {
        var attractingRigidBody = attractingObject.rigidBody;
        var direction = rigidBody.position - attractingRigidBody.position;
        var distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        }

        var forceMagnitude = G * (rigidBody.mass * attractingRigidBody.mass) / Mathf.Pow(distance, 2);
        var force = direction.normalized * forceMagnitude;

        attractingRigidBody.AddForce(force);
    }
}