using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
/// <summary>
/// The abstract class for all Inventory Interfaces the user interacts with
/// </summary>
public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory; // Player Inventory 
    public InventoryObject equipment; // Player Equipment
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>(); // The Slots in the interface

    /// <summary>
    /// Start method that is called on the item initializationg
    /// </summary>
    void Start()
    {
        //Get the Slots in the inventory
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;

        }
        //Make the slots and add the events to it
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        
       // UpdateSlots();
    }
    /// <summary>
    /// Event handler for when the slot is updated. Sets the image 
    /// </summary>
    /// <param name="_slot"></param>
    private void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.item.Id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    /// <summary>
    /// Abstract method for making item slots
    /// </summary>
    public abstract void CreateSlots();

    /// <summary>
    /// Add the event to the object
    /// </summary>
    /// <param name="obj"> Object </param>
    /// <param name="type"> Trigger Type </param>
    /// <param name="action"> The event Action </param>
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    /// <summary>
    /// Make sure when we enter an item slot, we set the item to the slot so we know what item we are on
    /// </summary>
    /// <param name="obj"></param>
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }
    /// <summary>
    /// Set the info on the mouse to nothing if theres no item
    /// </summary>
    /// <param name="obj"></param>
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }

    /// <summary>
    /// Sets the current interface data that the mouse is over
    /// </summary>
    /// <param name="obj"></param>
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    /// <summary>
    /// resets the interface data on the mouse
    /// </summary>
    /// <param name="obj"></param>
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    /// <summary>
    /// Add the item to the mouse when being dragged
    /// </summary>
    /// <param name="obj"></param>
    public void OnDragStart(GameObject obj)
    {

        MouseData.tempItemBeingDragged = CreateTempItem(obj);

    }
    /// <summary>
    /// Create the temp item to add to the mouse
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public GameObject CreateTempItem(GameObject obj) {
        GameObject tempItem = null;

        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }

        return tempItem;
    }
    /// <summary>
    /// Delete the item on the mouse and add it to the inventory
    /// </summary>
    /// <param name="obj"></param>
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);

        //Check if mouse is over an interface, to destroy or remove item from inventory
        if (MouseData.interfaceMouseIsOver == null) {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        //Check if mouse is over an Inventory Slot, to add to inventory or swap item
        if (MouseData.slotHoveredOver) {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }

        inventory.tempSave();
        equipment.tempSave();
    }
    public void OnDrag(GameObject obj){

        if (MouseData.tempItemBeingDragged != null){
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }

    }

}
/// <summary>
/// Mouse Object
/// </summary>
public static class MouseData
{
    public static UserInterface interfaceMouseIsOver; //Interface that the mouse is over
    public static GameObject tempItemBeingDragged; //Item being moved
    public static GameObject slotHoveredOver; //Slot being moved to

}
/// <summary>
/// Additional methods to add
/// </summary>
public static class ExtensionMethods 
{
    /// <summary>
    /// Update the item slots
    /// </summary>
    /// <param name="_slotsOnInterface"></param>
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface) {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            //If theres an item in the slot 

            if (_slot.Value.item.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

}
