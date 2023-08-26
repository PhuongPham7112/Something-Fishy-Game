using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerToCam : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        initialRotation.x = -initialRotation.x;
    }

    private void LateUpdate()
    {
        // Lock the child's rotation around the x-axis
        Vector3 eulerAngles = transform.localRotation.eulerAngles;
        eulerAngles.x = initialRotation.eulerAngles.x;
        transform.localRotation = Quaternion.Euler(eulerAngles);
    }
}
