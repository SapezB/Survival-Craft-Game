using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private Holder invetoryHolder;
    [SerializeField] protected UISlot[] slots;
    protected override void Start()
    {
        base.Start();
        
        if(invetoryHolder != null)
        {
            inventorySystem = invetoryHolder.PrimaryInvetorySystem;
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
