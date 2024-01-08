using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Invetory/Item")]
public class ItemData : ScriptableObject
{
   
    public Sprite Icon;
    public int MaxStackSize;
    public string Type;
    public GameObject prefab;
    public bool equipped = false;

    public int equippableItemIndex = 0;
  

    public static UnityAction<InventorySystem> OnPlayerHealthUpRequested;
    

}
