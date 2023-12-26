using System.Globalization;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;
    public DayNightCycles daytime;
    public Vector3 spawnAreaCenter;  
    public Vector3 spawnAreaSize;    
    public int maxSpawnCount = 10 ;   
    public float spawnInterval = 5f;
    public bool spawnOnStart = true; // Whether to start spawning objects immediately
    int currDay = 1;

    private void Start()
    {
        if (spawnOnStart)
        {
            StartSpawning();
        }
    }

    private void Update()
    {
       if(currDay < daytime.dayCount)
        {
            currDay = daytime.dayCount;
            maxSpawnCount = (int)Mathf.Round(maxSpawnCount * 1.5f);
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
        if (maxSpawnCount <= 0 || GameObject.FindGameObjectsWithTag("SpawnedObject").Length < maxSpawnCount)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
            );

            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}