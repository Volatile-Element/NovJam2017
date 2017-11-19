using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IPlayerInteractable
{
    public UnityEventFor<float> OnThrottleChanged = new UnityEventFor<float>();
    public UnityEventFor<string> OnVelocityChanged = new UnityEventFor<string>();

    private Camera playerCamera;

    public bool BeingControlled;

    private float _CurrentThrottle;
    public float CurrentThrottle { get { return _CurrentThrottle; } }

    public Gravity parent;
    public Rigidbody rigidBody;

    IShipInteractable interactableInRange;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rigidBody.velocity = (transform.forward * CurrentThrottle);
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (!BeingControlled)
        {
            return;
        }

        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.L) && interactableInRange != null)
        {
            interactableInRange.Interact();
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        float forwardThrust = Input.GetAxisRaw("Fire1");
        float backwardThrust = Input.GetAxisRaw("Fire2");

        _CurrentThrottle += (forwardThrust - backwardThrust);
        OnThrottleChanged.Invoke(_CurrentThrottle);
        OnVelocityChanged.Invoke(rigidBody.velocity.ToString());
            
        transform.Rotate(new Vector3(inputY, inputX, 0));
    }

    public void SetBeingControlled(bool state)
    {
        BeingControlled = state;

        playerCamera.gameObject.SetActive(BeingControlled);
        playerCamera.tag = BeingControlled ? "MainCamera" : "Untagged";
    }

    public void ParentToGravity(Gravity gravity)
    {
        parent = gravity;

        rigidBody.isKinematic = true;
        transform.parent = gravity.transform;
    }

    public void UnParent()
    {
        parent = null;
        rigidBody.isKinematic = false;
        transform.parent = null;
    }
    
    public void Interact()
    {
        UnParent();
        SetBeingControlled(true);
        FindObjectOfType<Player>().SetBeingControlled(false);
    }

    public void InteractionRangeEnter()
    {
        FindObjectOfType<UIInteraction>().Show("Press 'E' to Enter Ship");
    }

    public void InteractionRangeLeave()
    {
        FindObjectOfType<UIInteraction>().Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IShipInteractable>();
        if (interactable == null)
        {
            return;
        }

        interactable.InteractionRangeEnter();
        interactableInRange = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IShipInteractable>();
        if (interactable == null)
        {
            return;
        }

        interactable.InteractionRangeLeave();
        interactableInRange = null;
    }
}