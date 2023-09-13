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
            transform.Rotate(axes[openAxis], openDegree);
            isOpened = true;
            if (interactAudio != null)
            {
                interactAudio.Play();
            }
        } 
        else if (isOpened && isClicked)
        {
            ClearEffect();
            if (interactAudio != null)
            {
                interactAudio.Play();
            }
        }
    }

    public override void ClearEffect() 
    {
        // go back
        transform.rotation = originalRotation;
        isOpened = false;
    }
}
