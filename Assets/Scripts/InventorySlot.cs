using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Image img;
    public Color selectedCol, unselectedCol;

    private void Awake()
    {
        img = GetComponent<Image>();
        Deselect();
    }

    public void Select()
    {
        img.color = selectedCol;
    }

    public void Deselect()
    {
        img.color = unselectedCol;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // if slot is empty
        {
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            item.SetInitialParent(transform);
        }
    }
}

