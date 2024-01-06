using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Loot : ItemData
{
    public int dropChance;
    public GameObject prefab;


    public Loot( int dropChnce)
    {
        this.dropChance = dropChnce;
    }
}
