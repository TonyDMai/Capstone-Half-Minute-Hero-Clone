using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]//This allows to make new items from IDE
public class WeaponObject : ItemObject
{
    // Start is called before the first frame update

    public void Awake()
    {
        type = ItemType.Weapon;
    }
}
