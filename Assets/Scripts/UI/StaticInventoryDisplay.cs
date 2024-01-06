using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInvetoryDisplay : InventoryDisplay
{
    [SerializeField] private Holder invetoryHolder;
    [SerializeField] private UISlot[] slots;
    protected override void Start()
    {
        base.Start();
        
        if(invetoryHolder != null)
        {
            inventorySystem = invetoryHolder.InvetorySystem;
            inventorySystem.onInvetorySlotChanged += UpdateSlot;
        }

        AssignSlot(inventorySystem);
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        Slots = new Dictionary<UISlot, Slot>();

        for (int i = 0; i< inventorySystem.InventorySize; i++)
        {
            SlotDictionary.Add(slots[i], inventorySystem.slots[i]);
            slots[i].Init(inventorySystem.slots[i]);

        }

    }
}
