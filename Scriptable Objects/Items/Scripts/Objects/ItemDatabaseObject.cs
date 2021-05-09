using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that will keep track of the items for save and load
/// </summary>
[CreateAssetMenu (fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObjects;
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    [ContextMenu("UpdateIDs")]
    public void UpdateId() {
        for (int i = 0; i < ItemObjects.Length; i++)
        {
            if (ItemObjects[i].data.Id != i) {
                ItemObjects[i].data.Id = i;
            }
        }
    }

    //After Unity Serializes the object create a new dictionary that will hold all the items with an Id value; Makes a new Dictionary so theres no repeats.
    public void OnAfterDeserialize()
    {
        UpdateId();
    }
    /// <summary>
    /// Reset the dictionary before serializing
    /// </summary>
    public void OnBeforeSerialize()
    {
    }

}
