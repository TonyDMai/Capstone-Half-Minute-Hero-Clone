using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Types of Inventories Possible
/// </summary>
public enum InterfaceType { 
    Inventory,
    Equipment,
    Chest
}

/// <summary>
/// This is the Inventory Class, It stores objects that the player will use later
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    //Save locations
    public string savePath;
    public string tempSavePath;

    public ItemDatabaseObject database; //Item Database
    public InterfaceType type; //Type
    public Inventory container; // The Inventory
    // Inventory Slots
    public InventorySlot[] GetSlots {
        get {
            return container.Slots;
        }
    }

   /// <summary>
   /// Add an item to the inventory
   /// </summary>
   /// <param name="_item">item to add</param>
   /// <param name="_amount">amount to add</param>
   /// <returns></returns>
    public bool AddItem(Item _item, int _amount) {
        //Check if theres an availible slot for an item
        if (EmptySlotCount <= 0) { return false; }
        InventorySlot slot = FindItemOnInventory(_item);
        if (!database.ItemObjects[_item.Id].stackable || slot == null) {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }
    /// <summary>
    /// Get amount of empty slots left
    /// </summary>
    public int EmptySlotCount {
        get {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1) {
                    counter++;
                }
            }
            return counter;
        }
    }

    /// <summary>
    /// Find an item in the inventory
    /// </summary>
    /// <param name="_item">item to find</param>
    /// <returns>the item</returns>
    public InventorySlot FindItemOnInventory(Item _item) {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id) {
                return GetSlots[i];
            }
        }
        return null;
    }
    /// <summary>
    /// Add an Item to empty slot
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_amount"></param>
    /// <returns></returns>
    public InventorySlot SetEmptySlot(Item _item,int _amount) {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1) {
                GetSlots[i].UpdateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        //TODO: Set up what happens when inventory is full
        return null;
    }
    /// <summary>
    /// Swap Item positions in inventory
    /// </summary>
    /// <param name="item1">1st item to swap</param>
    /// <param name="item2">2nd item to swap</param>
    public void SwapItems(InventorySlot item1, InventorySlot item2) {

        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject)){
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }


    }

    /// <summary>
    /// Saves the inventory by converting the inventory to a String and write the string to a file 
    /// </summary>
    [ContextMenu("Save")]
    public void Save() {
        /// This top section will let you have editable save data
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        ///Harder to edit save
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
    }
    /// <summary>
    /// Temp Save
    /// </summary>
    public void tempSave() {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, tempSavePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
    }
    /// <summary>
    /// Method to load the inventory from a file
    /// </summary>
    [ContextMenu("Load")]
    public void Load() {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            ///Load Easy to edit Save
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            ///Load Hard to edit save
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);

            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }

            stream.Close();
        }
    }

    /// <summary>
    /// Loads temp data
    /// </summary>
    public void TempLoad() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, tempSavePath), FileMode.Open, FileAccess.Read);
        Inventory newContainer = (Inventory)formatter.Deserialize(stream);

        for (int i = 0; i < GetSlots.Length; i++)
        {
            GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
        }

        stream.Close();
    }

    /// <summary>
    /// Method to reset the inventory
    /// </summary>
    [ContextMenu("Clear")]
    public void Clear() {
        container.Clear();
    }
}



