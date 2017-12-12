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

    public GameObject ParticleEngine;
    public GameObject StarField;

    public AudioSource AudioSource;

    IShipInteractable interactableInRange;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 0, 0.525f));
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0, -0.525f));
        }

        rigidBody.velocity = (transform.forward * CurrentThrottle);

        if (rigidBody.velocity.magnitude >= 5 && !StarField.active)
        {
            StarField.SetActive(true);
        }

        if (rigidBody.velocity.magnitude < 5 && StarField.active)
        {
            StarField.SetActive(false);
        }
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
        if (Input.GetKeyDown(KeyCode.F) && interactableInRange != null)
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

        ParticleEngine.SetActive(state);

        if (state)
        {
            Camera.main.GetComponent<FollowCameraWithAnchor>().SetInterests(transform.Find("Camera Target"), transform.Find("Camera Anchor"));
            FindObjectOfType<Compass>().SetActivePlayer(transform);
            FindObjectOfType<Line>().SetActivePlayer(transform);

            AudioSource.Play();
        }
        else
        {
            AudioSource.Stop();
        }
    }

    public void ParentToGravity(Gravity gravity)
    {
        parent = gravity;

        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
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