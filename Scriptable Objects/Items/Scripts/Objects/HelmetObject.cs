using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Helmet Object", menuName = "Inventory System/Items/Helmet")]//This allows to make new items from IDE
public class HelmetObject : ItemObject
{
    // Start is called before the first frame update

    public void Awake()
    {
        type = ItemType.Helmet;
    }
}