using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableItem : InteractableFunctionality
{
    public int openAxis;
    public float openDegree;
    private bool isOpened = false;
    private Vector3[] axes = { Vector3.forward, Vector3.up, Vector3.right, Vector3.back, Vector3.left };
    private Quaternion originalRotation;
    private Rigidbody rb;
    void Start()
    {
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    public override void Action(bool isClicked, string key = "") 
    {
        if (isClicked && !isOpened) // this is a click only object
        {
            // rotate 90 degree
            Debug.Log("Rotating");
            transform.Rotate(axes[openAxis], openDegree);
            isOpened = true;
        } 
        else if (isOpened && isClicked)
        {
            ClearEffect();
        }
    }

    public override void ClearEffect() 
    {
        // go back
        transform.rotation = originalRotation;
        isOpened = false;
    }
}
