using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInvetoryDisplay : InventoryDisplay
{
    [SerializeField] protected UISlot slotPrefab;
    protected override void Start()
    {
        Holder.OnDynamicInvetoryDisplayRequested += RefreshDynamicInventory;
        base.Start();

        AssignSlot(inventorySystem);

    } 

    private void OnDestroy()
    {
        Holder.OnDynamicInvetoryDisplayRequested -= RefreshDynamicInventory;
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
            inventorySystem = invToDisplay;
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();

        Slots = new Dictionary<UISlot, Slot>();

        if(invToDisplay == null)
        {
            return;
        }

        for(int i = 0;i<invToDisplay.InventorySize;i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            Slots.Add(uiSlot, invToDisplay.Slots[i]);
            uiSlot.Init(invToDisplay.Slots[i]);
            uiSlot.UpdateUISlot();
        }
    }


    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>()) {
            Destroy(item.gameObject);

        }

        if(SlotDictionary != null)
        {
            SlotDictionary.Clear();
        }
    }


}
