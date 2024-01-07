
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeReference] MouseItemData mouseInvetoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<UISlot, Slot> Slots;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<UISlot, Slot> SlotDictionary => Slots;
    
    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(Slot updatedSlot)
    {
        foreach(var slot in Slots)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateUISlot(updatedSlot);
            }
        }
    }

    public void SlotClicked(UISlot clickedSlot){
        bool isZPressed = Keyboard.current.zKey.isPressed;
        if (clickedSlot.AssignedSlot.ItemData != null && mouseInvetoryItem.InvetorySlot.ItemData == null) 
        {
            

            if (isZPressed && clickedSlot.AssignedSlot.SplitStack(out Slot halfStack))
            {
                mouseInvetoryItem.UpdateMouseSlot(halfStack);
                clickedSlot.UpdateUISlot();
                return;
            }

            else
            {
                mouseInvetoryItem.UpdateMouseSlot(clickedSlot.AssignedSlot);
                clickedSlot.ClearSlot();
                return;
            }

            
           
        }


        if(clickedSlot.AssignedSlot.ItemData == null && mouseInvetoryItem.InvetorySlot.ItemData != null)
        {
            clickedSlot.AssignedSlot.AssignItem(mouseInvetoryItem.InvetorySlot);
            clickedSlot.UpdateUISlot();

            mouseInvetoryItem.ClearSlot();
            return;
        }


        if(clickedSlot.AssignedSlot.ItemData != null && mouseInvetoryItem.InvetorySlot.ItemData != null)
        {
            bool sameItem = clickedSlot.AssignedSlot.ItemData == mouseInvetoryItem.InvetorySlot.ItemData;
            if (sameItem && clickedSlot.AssignedSlot.RoomLeftInStack(mouseInvetoryItem.InvetorySlot.StackSize))
            {
                clickedSlot.AssignedSlot.AssignItem(mouseInvetoryItem.InvetorySlot);
                clickedSlot.UpdateUISlot();

                mouseInvetoryItem.ClearSlot();
                return;
            }
            else if(sameItem && !clickedSlot.AssignedSlot.RoomLeftInStack(mouseInvetoryItem.InvetorySlot.StackSize,out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedSlot);// Stack is full swap items
                else
                {
                    int remainingOnMouse = mouseInvetoryItem.InvetorySlot.StackSize - leftInStack;
                    clickedSlot.AssignedSlot.AddToStack(leftInStack);
                    clickedSlot.UpdateUISlot();

                    var newItem = new Slot(mouseInvetoryItem.InvetorySlot.ItemData, remainingOnMouse);
                    mouseInvetoryItem.ClearSlot();
                    mouseInvetoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            
            else if(!sameItem)
            {
                SwapSlots(clickedSlot);
                return;
            }
        }
    }

    private void SwapSlots(UISlot clickedSlot)
    {
        var clonedSlot = new Slot(mouseInvetoryItem.InvetorySlot.ItemData, mouseInvetoryItem.InvetorySlot.StackSize);
        mouseInvetoryItem.ClearSlot();

        mouseInvetoryItem.UpdateMouseSlot(clickedSlot.AssignedSlot);

        clickedSlot.ClearSlot();
        clickedSlot.AssignedSlot.AssignItem(clonedSlot);
        clickedSlot.UpdateUISlot();
    }

    
}
