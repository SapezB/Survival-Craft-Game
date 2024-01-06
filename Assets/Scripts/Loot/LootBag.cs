using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItem;
    public List<Loot> lootList = new List<Loot>();
   

    public Loot getDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();

        foreach(Loot item in lootList)
        {

            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
               
            }
        }

        if(possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
           
            return droppedItem;
        }

        Debug.Log("no loot dropped");
        return null;
    }

    public void InstantitateLoot(Vector3 spawnPosition)
    {
        Loot droppedItem = getDroppedItem();
       



        if (droppedItem != null )
        {
            GameObject lootObject = Instantiate(droppedItem.prefab, spawnPosition, Quaternion.identity);
          
        }
    }
}
