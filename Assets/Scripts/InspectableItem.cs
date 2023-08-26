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
    public float verticalSpeed;
    public float horizontalSpeed;
    public float zoomSpeed = 10.0f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // Check if the scroll input is not zero
        if (scrollInput != 0f)
        {
            float sign = Mathf.Sign(scrollInput);
            transform.localScale = ((transform.localScale.x < 200.0f && sign > 0.0f) || (transform.localScale.x > 50.0f && sign < 0.0f )) 
                ? transform.localScale + Mathf.Sign(scrollInput) * new Vector3(10.0f, 10.0f, 10.0f) 
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
