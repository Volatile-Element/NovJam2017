using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private float power = 50f;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(-transform.up * power);
    }
}