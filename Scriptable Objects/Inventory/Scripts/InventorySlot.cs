using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SlotUpdated(InventorySlot _slot);
/// <summary>
/// This is the Class responsible for storing Items in the inventory.
/// </summary>
[System.Serializable] // Show class in editor
public class InventorySlot
{
    public Item item; //Item
    public int amount; //Amount of the item
    [System.NonSerialized]
    public UserInterface parent; //Parent object
    [System.NonSerialized]
    public GameObject slotDisplay; // The item display
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate; // after update handler
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate; //before update handler
    public ItemType[] AllowedItems = new ItemType[0]; //Allowed Items in teh slot

    /// <summary>
    /// Gets the Item in the slot
    /// </summary>
    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }

    }
    /// <summary>
    /// Default constructor, makes an empty slot
    /// </summary>
    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    /// <summary>
    /// Make a new slot with an item in it
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_amount"></param>
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }
    /// <summary>
    /// Add the amount to the inventory slot
    /// </summary>
    /// <param name="value"></param>
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    /// <summary>
    /// Update the slot with the new item and amount
    /// </summary>
    /// <param name="_item">item to add </param>
    /// <param name="_amount">amount to add </param>
    public void UpdateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null) { OnBeforeUpdate.Invoke(this); }
        item = _item;
        amount = _amount;
        if (OnAfterUpdate != null) { OnAfterUpdate.Invoke(this); }

    }
    /// <summary>
    /// Remove Item from inventory
    /// </summary>
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }

    /// <summary>
    /// Check if the item can go in the slot
    /// </summary>
    /// <param name="_itemObject"></param>
    /// <returns></returns>
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
        {
            return true;
        }
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}