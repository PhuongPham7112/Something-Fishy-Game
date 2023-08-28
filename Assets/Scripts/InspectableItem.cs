using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [SerializeField] Transform inspectPoint;
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;

    [SerializeField] private Vector2 turn;

    [Header("Physics")]
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;

    // Update is called once per frame
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // Check if the scroll input is not zero
        if (scrollInput != 0f)
        {
            float sign = Mathf.Sign(scrollInput);
            transform.localScale = ((transform.localScale.x < maxZoom && sign > 0.0f) || (transform.localScale.x > minZoom && sign < 0.0f )) 
                ? transform.localScale + Mathf.Sign(scrollInput) * new Vector3(zoomSpeed, zoomSpeed, zoomSpeed) 
                : transform.localScale;
        }

        if (Input.GetMouseButton(0))
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");
            transform.localRotation = Quaternion.Euler(turn.y * verticalSpeed, turn.x * horizontalSpeed, 0);
        }
        
    }
}
