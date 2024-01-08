using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class DynamicInvetoryDisplay : InventoryDisplay
{
    [SerializeField] protected UISlot slotPrefab;
    protected override void Start()
    {

        base.Start();



    }



    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null) inventorySystem.onInvetorySlotChanged += UpdateSlot;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {

        Slots = new Dictionary<UISlot, Slot>();

        if (invToDisplay == null)
        {
            return;
        }

        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            
            var uiSlot = Instantiate(slotPrefab, transform);
            Slots.Add(uiSlot, invToDisplay.slots[i]);
            uiSlot.Init(invToDisplay.slots[i]);
            uiSlot.UpdateUISlot();
        }
    }


    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);

        }

        if (Slots != null)
        {
            Slots.Clear();
        }
    }

    private void OnDisable()
    {
        if (inventorySystem != null) inventorySystem.onInvetorySlotChanged -= UpdateSlot;
    }

}