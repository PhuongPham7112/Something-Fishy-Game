using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableItem : InteractableFunctionality
{
    public int pullDirection;
    public float pullDegree;
    private bool isPulled = false;
    private Vector3 originalPosition;
    private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.right, Vector3.left };

    void Start()
    {
        
    }
    public override void Action(bool isClicked, string key = "")
    {
        if (isClicked && !isPulled) // this is a click only object
        {
            Debug.Log("Pulling");
            // go forward by 20 unit
            originalPosition = transform.position;
            transform.Translate(directions[pullDirection] * pullDegree);
            isPulled = true;
        } else if (isClicked && isPulled)
        {
            ClearEffect();
        }
    }

    public override void ClearEffect()
    {
        // go back
        transform.position = originalPosition;
        isPulled = false;
    }
}
