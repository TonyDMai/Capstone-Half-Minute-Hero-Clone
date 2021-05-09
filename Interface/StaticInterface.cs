using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    public override void CreateSlots()
    {
        //New dictionary of equipped Items
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        //goes through all equipment in Database
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = slots[i]; // Object that Links to array which links to actual gameobjects in the scene

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }

    // Start is called before the first frame update

}
