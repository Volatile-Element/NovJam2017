using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera playerCamera;

    public bool BeingControlled;

    public Gravity parent;
    
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 15;
    public float jumpForce = 220;
    public LayerMask groundedMask;

    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;

    IPlayerInteractable interactableInRange;

    public GameObject BoosterPrefab;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        playerCamera = GetComponentInChildren<Camera>();
        cameraTransform = playerCamera.transform;
    }

	private void FixedUpdate()
    {
        //Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        //rigidbody.MovePosition(rigidbody.position + localMove);

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        transform.RotateAround(parent.transform.position, transform.forward, (-inputX) / parent.transform.Find("Model").localScale.x * 15f);
        transform.RotateAround(parent.transform.position, transform.right, (inputY) / parent.transform.Find("Model").localScale.x * 15f);

        if (parent == null)
        {
            return;
        }

        var gravityUp = (transform.position - parent.transform.position).normalized;
        var localUp = transform.up;

        parent.Attract(GetComponent<Gravity>());

        transform.rotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
    }

    private void Update()
    {
        ProcessInput();
    }

    public void ParentToGravity(Gravity gravity)
    {
        parent = gravity;

        rigidbody.velocity = gravity.GetComponent<Rigidbody>().velocity;

        GetComponent<Gravity>().GravityDisabled = true;
    }

    public void UnParent()
    {
        parent = null;
    }

    private void ProcessInput()
    {
        if (!BeingControlled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && interactableInRange != null)
        {
            interactableInRange.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hits;
            Debug.DrawRay(transform.position, -transform.up);
            var result = Physics.Raycast(transform.position, -transform.up, out hits, Mathf.Infinity);
            if (result)
            {
                var booster = Instantiate(BoosterPrefab, hits.point, transform.rotation, parent.transform);
            }
        }

        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        //verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        //cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidbody.AddForce(transform.up * jumpForce);
            }
        }

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2 + .1f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void SetBeingControlled(bool state)
    {
        BeingControlled = state;

        playerCamera.gameObject.SetActive(BeingControlled);
        playerCamera.tag = BeingControlled ? "MainCamera" : "Untagged";
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IPlayerInteractable>();
        if (interactable == null)
        {
            return;
        }

        interactable.InteractionRangeEnter();
        interactableInRange = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IPlayerInteractable>();
        if (interactable == null)
        {
            return;
        }

        interactable.InteractionRangeLeave();
        interactableInRange = null;
    }
}