using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player Inventory object
/// </summary>
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[40]; //array of all the inventory slots
    /// <summary>
    /// Clear Inventory
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}