using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager instance;
    public Camera inspectCam;
    
    private Camera regularCam;
    public bool inViewMode = false;
    private bool finishSwitch = false;
    private GameObject currentView;

    // Start is called before the first frame update
    void Start()
    {
        regularCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inViewMode = !inViewMode;
        }

        if (inViewMode)
        {
            if (!finishSwitch)
            {
                regularCam.enabled = false;
                inspectCam.enabled = true;
                finishSwitch = true;
                InventoryManager.instance.isLocked = true;
                Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
                if (selectedItem != null)
                {
                    Vector3 worldPoint = inspectCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, inspectCam.nearClipPlane));
                    currentView = ObjectPool.Instance.SpawnFromPool(selectedItem.itemModelTag, new Vector3(worldPoint.x, worldPoint.y, 0) + inspectCam.transform.forward * 10.0f, Quaternion.identity);
                    currentView.GetComponent<Rigidbody>().isKinematic = true;
                    currentView.GetComponent<InspectableItem>().enabled = true;
                }
            }
        }
        else
        {
            InventoryManager.instance.isLocked = false;
            inspectCam.enabled = false;
            regularCam.enabled = true;
            if (currentView != null) {
                currentView.GetComponent<Poolable>().ReturnToPool();
                currentView.GetComponent<InspectableItem>().enabled = false;
            } 
            finishSwitch = false;
        }
    }
}
