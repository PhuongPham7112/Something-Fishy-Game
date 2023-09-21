using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [SerializeField] Transform inspectPoint;
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;
    [SerializeField] float zoomX = 1f;
    [SerializeField] float zoomY = 1f;
    [SerializeField] float zoomZ = 1f;
    [SerializeField] private Vector2 turn;

    [Header("Physics")]
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;
    public float zoomSpeed = 1f;
    public Vector3 zoom;

    private void Awake()
    {
        minZoom = transform.localScale.x;
        maxZoom = minZoom * 3;
        float perZoom = minZoom / 10;
        zoom = new Vector3(perZoom * zoomSpeed, perZoom * zoomSpeed, perZoom * zoomSpeed);
        enabled = false;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // Check if the scroll input is not zero
        if (scrollInput != 0f)
        {
            float sign = Mathf.Sign(scrollInput);
            
            transform.localScale = ((transform.localScale.x < maxZoom && sign > 0.0f) || (transform.localScale.x > minZoom && sign < 0.0f )) 
                ? transform.localScale + Mathf.Sign(scrollInput) * zoom
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
