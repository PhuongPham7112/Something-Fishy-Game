using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    
    public Item item; // what item is this object associated with
    private GameObject player;
    private Color startcolor;
    private Renderer objRenderer;
    private float maxDist = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        startcolor = objRenderer.material.color;
        player = PlayerSingleton.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // check if clicked
        if (Vector3.Distance(transform.position, player.transform.position) < maxDist)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Interactable") // if clicked, put in to slot
                    {
                        if (InventoryManager.instance.AddItem(item))
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    void OnMouseEnter()
    {
        // highlight
        if (Vector3.Distance(transform.position, player.transform.position) < maxDist)
        {
            objRenderer.material.color = Color.yellow;
        }
        
    }
    void OnMouseExit()
    {
        objRenderer.material.color = startcolor;
    }

    
}
