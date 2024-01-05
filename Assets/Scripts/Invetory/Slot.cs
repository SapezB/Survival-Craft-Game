using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot 
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int stackSize;

    public ItemData ItemData => itemData;

    public int StackSize => stackSize;  

    public Slot(ItemData source,int amount)
    {
        itemData = source;
        stackSize = amount;
    }


    public Slot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void UpdateSlot(ItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }


    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AssignItem(Slot invSlot)
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);

        }
    }

    public bool SplitStack(out Slot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new Slot(itemData,halfStack);
        return true;
    }
}
