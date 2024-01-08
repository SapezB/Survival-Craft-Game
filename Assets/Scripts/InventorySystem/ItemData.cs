using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Invetory/Item")]
public class ItemData : ScriptableObject
{
    public Sprite Icon;
    public int MaxStackSize;
    public string Type;
    public GameObject prefab;
    public bool equipped = false;
    public int equippableItemIndex = 1;

    public new string name = "New Item";
    public string description = "New Description";
    public int currentQuantity = 1;
}
