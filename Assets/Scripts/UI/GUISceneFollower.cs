using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISceneFollower : MonoBehaviour
{
    public Transform FollowThis;

    private Vector3 screenPos;
    private Vector2 onScreenPos;
    private float max;

    void Update()
    {
        if (FollowThis == null)
        {
            return;
        }

        Vector2 sp = Camera.main.WorldToScreenPoint(FollowThis.position);

        var screenPos = Camera.main.WorldToViewportPoint(FollowThis.position);

        if (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1)
        {
            transform.position = sp;
            return;
        }

        onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

        transform.position = onScreenPos;
    }

    void OnEnable()
    {
        // this is here because there can be a single frame where the position is incorrect
        // when the object (or its parent) is activated.
        if (gameObject.activeInHierarchy)
        {
            Update();
        }
    }
}