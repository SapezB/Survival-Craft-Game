using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerHolder : EquipmentHolder
{
  
    [SerializeField] protected int thirdInventroySize;


    [SerializeField] protected InventorySystem thirdInvetroySystem;

    
    public InventorySystem ThirdInvetroySystem => thirdInvetroySystem;


    public GameObject player;
    public List<Recipe> itemRecipes = new List<Recipe>();

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    
    protected override void Awake()
    {
        base.Awake();

 
        thirdInvetroySystem = new InventorySystem(thirdInventroySize);

    }
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            OnPlayerEquipmentDisplayRequested?.Invoke(secondaryInvetroySystem);
            OnPlayerBackpackDisplayRequested?.Invoke(thirdInvetroySystem);
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            OnPlayerEquipmentDisplayRequested?.Invoke(secondaryInvetroySystem);
            OnPlayerBackpackDisplayRequested?.Invoke(thirdInvetroySystem);
        }





    }


    public bool AddToInventory(ItemData data ,int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount)) return true;

        else if(thirdInvetroySystem.AddToInventory(data, amount)) { return true; }


        return false;
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
