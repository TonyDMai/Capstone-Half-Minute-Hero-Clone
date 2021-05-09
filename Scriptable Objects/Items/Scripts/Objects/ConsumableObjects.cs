using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]//This allows to make new items from IDE
public class ConsumableObjects : ItemObject
{
    public int restoreHealthValue;
    // Start is called before the first frame update
    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
