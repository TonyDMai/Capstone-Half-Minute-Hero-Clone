using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Shield Object", menuName = "Inventory System/Items/Shield")]//This allows to make new items from IDE
public class ShieldObject: ItemObject
{
    // Start is called before the first frame update

    public void Awake()
    {
        type = ItemType.Shield;
    }
}