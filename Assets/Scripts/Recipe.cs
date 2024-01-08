using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe", menuName = "Survival-Craft-Game/Recipe", order = 1)]
public class Recipe : ScriptableObject {
        public GameObject createdItem;
        public int amountMade = 1;
        public List<requiredIngredients> requiredIngredients = new List<requiredIngredients>();
}


[System.Serializable]
public class requiredIngredients{
    public string itemName;
    public int amountNeeded;
}