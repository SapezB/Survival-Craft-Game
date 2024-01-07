using System.Globalization;
using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject skeleton;
    public GameObject sheep;
    // public GameObject goat;
    public LightManager daytime;
    public Vector3 spawnAreaCenter;  
    public Vector3 spawnAreaSize;    
    public int maxEnemy = 20 ;   
    public int maxPassive = 100 ; 
    public float spawnInterval = 5f;
    public bool spawnOnStart = true; // Whether to start spawning objects immediately
    private int currDay = 0;
    [SerializeField] private bool night;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    private void Update()
    {
       if(currDay < daytime.numDays)
        {
            currDay = daytime.numDays;
            maxEnemy = (int)Mathf.Round(maxEnemy * 1.5f);
        }
        if(daytime.currTime < 4  || daytime.currTime >= 20 ){
            night = true;
        }
        else{
            night = false;
            foreach(var enemy in spawnedEnemies){
                Destroy(enemy);
            }
        }
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnObject");
    }

    private void SpawnObject()
    {
        Vector3 randomPosition = new Vector3(
        Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
        Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
        Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2) 
        );

        if(night){
            if (maxEnemy <= 0 || GameObject.FindGameObjectsWithTag("SpawnedEnemy").Length < maxEnemy){}
                var enemy = Instantiate(skeleton, randomPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        else if (maxPassive <= 0 || GameObject.FindGameObjectsWithTag("SpawnedPassive").Length < maxPassive){
            //     if(Random.Range(0,2) == 1){
            //         Instantiate(goat, randomPosition, Quaternion.identity);
            //     }
            //     else{
            //         Instantiate(sheep, randomPosition, Quaternion.identity);
            //     }
            // }   
            Instantiate(sheep, randomPosition, Quaternion.identity);
        }
}
}