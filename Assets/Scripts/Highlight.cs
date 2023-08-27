using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{

    [SerializeField] public Item item = null; // what item is this object associated with
    [SerializeField] public InteractableFunctionality actionFunction = null;
    [SerializeField] public string lockWord = null;
    [SerializeField] public InteractableFunctionality secondActionFunction = null;
    private GameObject player;
    private Material originalMaterial;
    private Material highlightMaterial;
    private Renderer objRenderer;
    private float maxDist = 10.0f;
    private Poolable poolable;

    // Start is called before the first frame update
    void Start()
    {
        // cache original material
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;

        // Create a new material instance for highlighting
        highlightMaterial = new Material(originalMaterial);
        highlightMaterial.EnableKeyword("_EMISSION");

        // object pooling
        player = PlayerSingleton.Instance.gameObject;
        poolable = GetComponent<Poolable>();

        
        if (actionFunction != null && lockWord != null && secondActionFunction != null)
        {
            Debug.Log("Requires keyword " + lockWord);
            actionFunction.SetRequirement(lockWord, secondActionFunction); // set all the requirements
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        // check if clicked
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDist))
            {
                if (hit.transform.gameObject == gameObject) // if this is clicked
                {
                    Debug.Log("Clicked " + hit.transform.name);
                    // decide whether to take action or collect item
                    if (item != null && InventoryManager.instance.AddItem(item))
                    {
                        Debug.Log("Add to inventory");
                        poolable.ReturnToPool();
                    } else if (actionFunction != null) // call the action on object otherwise
                    {
                        Debug.Log("Cannot store item, try action instead");
                        CallAction(true);
                    }
                    else
                    {
                        Debug.Log("There's no action to this");
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
            // Set the emissive color to make the object appear highlighted
            highlightMaterial.SetColor("_EmissionColor", Color.white * 0.2f);

            // Apply the highlight material to the renderer
            objRenderer.material = highlightMaterial;
        }
        
    }
    void OnMouseExit()
    {
        // Restore the original material
        objRenderer.material = originalMaterial;
    }

    public void CallAction(bool isClicked, string key = "")
    {
        if (actionFunction != null)
        {
            Debug.Log("Successfully call action");
            actionFunction.Action(isClicked, key);
        }
    }
}
