using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    private Quaternion initialRotation;
    private Transform parentTransform;

    private void Start()
    {
        // Store the initial rotation of the child object.
        initialRotation = transform.localRotation;

        // Find and store the parent transform.
        parentTransform = transform.parent;
    }

    private void LateUpdate()
    {
        // Counteract the parent's rotation by applying the inverse.
        transform.rotation = parentTransform.rotation * initialRotation;
    }
}
