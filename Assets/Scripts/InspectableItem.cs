using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : MonoBehaviour
{
    [SerializeField] Transform inspectPoint;
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;

    [Header("Physics")]
    public float verticalSpeed;
    public float horizontalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
