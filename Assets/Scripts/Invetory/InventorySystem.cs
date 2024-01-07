using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class InventorySystem 
{
    [SerializeField] private List<Slot> inventorySlots;

    


    public List<Slot> slots => inventorySlots;

    public int InventorySize => inventorySlots.Count;

    public UnityAction<Slot> onInvetorySlotChanged;


    public InventorySystem(int size)
    {
        inventorySlots = new List<Slot>(size);

        for (int i = 0; i< size; i++)
        {
            inventorySlots.Add(new Slot());
        }
    }

    public bool AddToInventory(ItemData itemToAdd,  int amountToadd)
    {
      if (ContainsItem(itemToAdd, out List<Slot> invSlot))
        {
            foreach(var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToadd))
                {
                    slot.AddToStack(amountToadd);
                    onInvetorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }
     if (HasFreeSlot(out Slot freeSlot))
        {
            freeSlot.UpdateSlot(itemToAdd, amountToadd);
            onInvetorySlotChanged?.Invoke(freeSlot);
            return true;
        }
      return false;
    }



    public bool ContainsItem(ItemData itemToAdd,out List<Slot> invSlot) {

        invSlot = inventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        
        return invSlot ==  null ? false : true;
    }


    public bool HasFreeSlot(out Slot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
