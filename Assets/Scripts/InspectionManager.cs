using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager instance;
    public Camera inspectCam;
    public InspectionPanel inspectionPanel;
    
    private Camera regularCam;
    public bool inViewMode = false;
    private bool finishSwitch = false;
    private GameObject currentView;
    private Item currentItem;

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
            inViewMode = true;
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            inViewMode = false;
        }

        if (inViewMode && !InventoryManager.instance.IsEmpty())
        {
            Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
            Vector3 worldPoint = inspectCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, inspectCam.nearClipPlane));
            if (!finishSwitch)
            {
                Debug.Log("Switch mode");
                regularCam.enabled = false;
                inspectCam.enabled = true;
                InventoryManager.instance.isLocked = true;
                inspectionPanel.ChangePanelVisibility(true);
                if (selectedItem != null)
                {
                    currentItem = selectedItem;
                    currentView = ObjectPool.Instance.SpawnFromPool(selectedItem.itemModelTag, new Vector3(worldPoint.x, worldPoint.y, 0) + inspectCam.transform.forward * 10.0f, Quaternion.identity);
                    currentView.GetComponent<InspectableItem>().enabled = true;
                    currentView.GetComponent<Highlight>().enabled = false;
                    currentView.GetComponent<Rigidbody>().isKinematic = true;
                    currentView.transform.GetChild(0).gameObject.SetActive(false); // noted, let hover text be the first child

                }
                finishSwitch = true;
            }
            else if (selectedItem != null && selectedItem != currentItem) // newly updated selected item
            {
                Debug.Log("Update current view");
                currentView.GetComponent<Poolable>().ReturnToPool();
                currentView.GetComponent<InspectableItem>().enabled = false;
                currentView.GetComponent<Highlight>().enabled = true;

                GameObject newView = ObjectPool.Instance.SpawnFromPool(selectedItem.itemModelTag, new Vector3(worldPoint.x, worldPoint.y, 0) + inspectCam.transform.forward * 10.0f, Quaternion.identity);
                newView.GetComponent<InspectableItem>().enabled = true;
                newView.GetComponent<Highlight>().enabled = false;
                newView.GetComponent<Rigidbody>().isKinematic = true;
                newView.transform.GetChild(0).gameObject.SetActive(false);

                currentView = newView;
                currentItem = selectedItem;
            }
            inspectionPanel.ChangeText(currentItem.readableContent);
        }
        else
        {
            InventoryManager.instance.isLocked = false;
            inspectCam.enabled = false;
            regularCam.enabled = true;
            inspectionPanel.ChangePanelVisibility(false);
            inspectionPanel.ChangeText("");
            if (currentView != null) {
                currentView.GetComponent<Poolable>().ReturnToPool();
                currentView.GetComponent<InspectableItem>().enabled = false;
                currentView.GetComponent<Highlight>().enabled = true;
                currentView.transform.GetChild(0).gameObject.SetActive(true);
            } 
            finishSwitch = false;
        }
    }
}
