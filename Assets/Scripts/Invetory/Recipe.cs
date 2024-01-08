using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{

    public GameObject createItem;
    public int amountMade = 1;
    public List<requiredIngridinets> requiredIngredients = new List<requiredIngridinets> { };
}
[System.Serializable]


public class requiredIngridinets
{
    public string itemName;
    public int amountNeeded;
}
