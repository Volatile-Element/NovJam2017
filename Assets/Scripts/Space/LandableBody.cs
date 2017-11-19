using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandableBody : MonoBehaviour, IShipInteractable
{
    public Vector3 ShipLandingPoint;

    public float InteractionRange;

    private Gravity gravity;
    private SphereCollider triggerZone;

    private void Awake()
    {
        gravity = transform.parent.GetComponent<Gravity>();
        triggerZone = GetComponent<SphereCollider>();

        SetScale(transform.parent.Find("Model").localScale);
    }

    public void Interact()
    {
        FindObjectOfType<Ship>().transform.position = transform.position + ShipLandingPoint;
        FindObjectOfType<Ship>().transform.rotation = Quaternion.Euler(0, 0, 0);
        FindObjectOfType<Ship>().ParentToGravity(gravity);
        FindObjectOfType<Player>().ParentToGravity(gravity);
        FindObjectOfType<Player>().transform.position = transform.position + ShipLandingPoint;
        FindObjectOfType<Player>().SetBeingControlled(true);
        FindObjectOfType<Ship>().SetBeingControlled(false);
    }

    public void InteractionRangeEnter()
    {
        FindObjectOfType<UIInteraction>().Show("Press 'L' to Land Ship");
    }

    public void InteractionRangeLeave()
    {
        FindObjectOfType<UIInteraction>().Hide();
    }

    public void SetScale(Vector3 scale)
    {
        ShipLandingPoint = new Vector3(0, scale.y / 2, 0);
        triggerZone.radius = scale.y / 2 + InteractionRange;
    }
}