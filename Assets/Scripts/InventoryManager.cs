using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventorySlot[] slots;
    public GameObject inventoryItemPrefab;
    public bool isLocked = false;

    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelectedSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelectedSlot(1);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSelectedSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSelectedSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSelectedSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSelectedSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSelectedSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeSelectedSlot(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeSelectedSlot(8);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedSlot > 0)
        {
            ChangeSelectedSlot(selectedSlot - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && selectedSlot < slots.Length - 1)
        {
            ChangeSelectedSlot(selectedSlot + 1);
        }
    }

    void ChangeSelectedSlot(int newSlot)
    {
        if (selectedSlot > -1)
        {
            slots[selectedSlot].Deselect();
        }
        slots[newSlot].Select();
        selectedSlot = newSlot;
    }

    public bool AddItem(Item newItem) // return whether still has space or not
    {
        // find an empty slot
        foreach (InventorySlot slot in slots) {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(newItem, slot);
                return true;
            }
        }
        return false;
    }

    public Item GetSelectedItem(bool discard)
    {

        InventoryItem itemInSlot = slots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            if (discard)
            {
                Destroy(itemInSlot.gameObject);
            }
            return itemInSlot.item;
        }
        return null;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newInvItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newInvItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
}
