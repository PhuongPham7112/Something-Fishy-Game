using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispendItem : InteractableFunctionality
{
    public Item droppingItem;

    public override void Action(bool isClicked, string key = "")
    {
        // dispense withheld item to the player's inventory
        InventoryManager.instance.AddItem(droppingItem);
    }
}
