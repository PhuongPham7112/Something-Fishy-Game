using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Highlight : MonoBehaviour
{

    [SerializeField] public Item item = null; // what item is this object associated with
    [SerializeField] public InteractableFunctionality actionFunction = null;
    [SerializeField] public string lockWord = null;
    [SerializeField] public InteractableFunctionality secondActionFunction = null;
    [SerializeField] public TextMeshPro hoverText;
    [SerializeField] public AudioSource pickupAudio = null;

    private GameObject player;
    private float maxDist = 10.0f;
    private Poolable poolable;
    private ConvoTrigger convo;

    // Start is called before the first frame update
    void Start()
    {
        // object pooling
        player = PlayerSingleton.Instance.gameObject;
        poolable = GetComponent<Poolable>();

        if (item != null)
            lockWord = item.itemUnlockKey;

        if (actionFunction != null && !string.IsNullOrEmpty(lockWord))
        {
            actionFunction.SetRequirement(lockWord, secondActionFunction); // set all the requirements
        }
        hoverText.enabled = false;
        convo = GetComponent<ConvoTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if clicked
        if (!InventoryManager.instance.isLocked && Input.GetMouseButtonDown(0)) // if click while not in inspection mode
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDist))
            {
                if (hit.transform.gameObject == gameObject) // if this is clicked
                {
                    Debug.Log("Clicked " + hit.transform.name);
                    // If there's dialogue cue, play it
                    if (convo != null)
                    {
                        convo.PlayDialogue();
                    }
                    // decide whether to take action or collect item
                    if (item != null && InventoryManager.instance.AddItem(item, pickupAudio))
                    {
                        if (!pickupAudio.isPlaying) pickupAudio.Play();
                        poolable.ReturnToPool();
                    } 
                    else if (actionFunction != null) // call the action on object otherwise
                    {
                        CallAction(true); // click on object
                    }
                }
            }
        }
    }

    void OnMouseEnter()
    {
        // highlight on mouse hover
        if (!InventoryManager.instance.isLocked && Vector3.Distance(transform.position, player.transform.position) < maxDist)
        {
            hoverText.enabled = true;
        }
        
    }
    void OnMouseExit()
    {
        hoverText.enabled = false;
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
