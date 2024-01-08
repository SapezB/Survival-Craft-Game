using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour {
    
    [SerializeField] private List<Slot> allInventorySlots;
   
    [SerializeField] private InventorySystem hotbarSlots;
    public List<Recipe> itemRecipes = new List<Recipe>();
    public GameObject player;


    




    public List<Slot> slots => allInventorySlots;

    public int InventorySize => allInventorySlots.Count;

    public UnityAction<Slot> onInvetorySlotChanged;


    public InventorySystem(int size)
    {
        allInventorySlots = new List<Slot>(size);

        for (int i = 0; i < size; i++)
        {
            allInventorySlots.Add(new Slot());
        }

       
    }

    public bool AddToInventory(ItemData itemToAdd, int amountToadd)
    {
        if (ContainsItem(itemToAdd, out List<Slot> invSlot))
        {
            foreach (var slot in invSlot)
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



    public bool ContainsItem(ItemData itemToAdd, out List<Slot> invSlot)
    {

        invSlot = allInventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        return invSlot == null ? false : true;
    }


    public bool HasFreeSlot(out Slot freeSlot)
    {
        freeSlot = allInventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

    //Crafting
    public void craftItem(string itemName){
        foreach(Recipe recipe in itemRecipes){
            if(recipe.createdItem.GetComponent<ItemData>().name == itemName)
            {
                bool haveAllIngredients = true;
                for(int i = 0; i < recipe.requiredIngredients.Count; i++){
                    if(haveAllIngredients){
                        haveAllIngredients = haveIngredient(recipe.requiredIngredients[i].itemName, recipe.requiredIngredients[i].amountNeeded);
                    }
                }

                if(haveAllIngredients){
                    for(int i = 0; i < recipe.requiredIngredients.Count; i++){
                        removeIngredient(recipe.requiredIngredients[i].itemName, recipe.requiredIngredients[i].amountNeeded);
                    }
                    GameObject craftedItem = Instantiate(recipe.createdItem, player.transform.position, Quaternion.identity);
                    craftedItem.GetComponent<ItemData>().currentQuantity = recipe.amountMade;

                    AddToInventory(craftedItem.GetComponent<ItemData>(),craftedItem.GetComponent<ItemData>().currentQuantity);
                }

                break;
            }
        }
    }

    private void removeIngredient(string itemName, int amount){
        if(!haveIngredient(itemName,amount)){
            return;
        }

        int remainingAmount = amount;

        foreach(Slot curslot in allInventorySlots){
            ItemData item = curslot.getItem();

            if(item != null && item.name == itemName){
                if(item.currentQuantity >= remainingAmount){
                    item.currentQuantity -= remainingAmount;

                    if(item.currentQuantity == 0){
                        curslot.ClearSlot();
                    }
                    return;
                }
                else{
                    remainingAmount -= item.currentQuantity;
                    curslot.ClearSlot();
                }
            }

        }
    }

    private bool haveIngredient(string itemName, int amount){
        int foundAmount = 0;
        foreach(Slot curslot in allInventorySlots){
            if(curslot.hasItem() && curslot.getItem().name == itemName){
                foundAmount += curslot.getItem().currentQuantity;
                
                if(foundAmount >= amount){
                    return true;
                }
            }
        }
        return false;
    }
}
