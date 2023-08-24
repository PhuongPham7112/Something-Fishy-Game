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
            Debug.Log("Switch mode");
            inViewMode = !inViewMode;
        }

        if (inViewMode)
        {
            if (!finishSwitch)
            {
                regularCam.enabled = false;
                inspectCam.enabled = true;
                finishSwitch = true;
                Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
                if (selectedItem != null)
                {
                    Debug.Log("Observe! " + selectedItem.name);

                    Vector3 worldPoint = inspectCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, inspectCam.nearClipPlane));
                    currentView = Instantiate(selectedItem.itemModel, new Vector3(worldPoint.x, worldPoint.y, 0) + inspectCam.transform.forward * 10.0f, Quaternion.identity);
                    currentView.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        else
        {
            inspectCam.enabled = false;
            regularCam.enabled = true;
            Destroy(currentView);
            finishSwitch = false;
        }
    }
}
