using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    Consumable, // For Future expansion
    Helmet,
    Weapon,
    Shield,// Only Equipment will be used here
    Gold,
    Default
}

public enum Attributes { 
    Attack,
    Defense
}

public abstract class ItemObject : ScriptableObject
{
    public Sprite uiDisplay; //Item image
    public bool stackable;
    public ItemType type; // Item type
    [TextArea(15,20)]
    public string description;
    public int sellGold;
    public int buyGold;
    public Item data = new Item();

    public Item CreateItem() {
        Item newItem = new Item(this);
        return newItem;
    }
}


