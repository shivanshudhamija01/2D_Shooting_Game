using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 1. Enemy Type (Prefab)
    // 2. Spawning Time
    // 3. Maximum number of this particular type agent 
    // 4. Time since previuos spawn 
    // 5. Spawning Point 
    // 6. Queue for storing the last spawned enemy 
    // 7. Next enemy to be spawned depend upon whether the enemy is present in the deadEnemies or not  

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawningTime;
    [SerializeField] private int maximumSpawnCount;
    [SerializeField] private Transform[] spawningPoints;

    private List<GameObject> deadEnemies = new List<GameObject>();
    private List<GameObject> enemiesInCurrentScene = new List<GameObject>();
    private float spawnedEnemiesCount = 0f;
    private float timeSincePreviousSpawn = 0;

    void Start()
    {
        // int random = Random.Range(0, spawningPoints.Length);
        // GameObject enemyObj = Instantiate(enemyPrefab, spawningPoints[random].position, Quaternion.identity);
        // timeSincePreviousSpawn = 0;
        // spawnedEnemiesCount = 1f;
        // enemiesInCurrentScene.Add(enemyObj);
    }

    void Update()
    {
        int random = Random.Range(0,spawningPoints.Length);
        timeSincePreviousSpawn += Time.deltaTime;
        if (timeSincePreviousSpawn > spawningTime)
        {
            if (deadEnemies.Count > 0)
            {
                GameObject enemyObj = deadEnemies[0];
                enemyObj.transform.position = spawningPoints[random].position;
                enemyObj.SetActive(true);
                deadEnemies.RemoveAt(0);
                timeSincePreviousSpawn = 0;
            }
            else if (spawnedEnemiesCount < maximumSpawnCount)
            {
                GameObject enemyObj = Instantiate(enemyPrefab, spawningPoints[random].position, Quaternion.identity);
                timeSincePreviousSpawn = 0;
                spawnedEnemiesCount++;
                enemiesInCurrentScene.Add(enemyObj);
            }
            else
            {
                timeSincePreviousSpawn = 0f;
            }
        }
    }

    void IAmDeadNow(GameObject enemyObj)
    {
        enemiesInCurrentScene.Remove(enemyObj);
        deadEnemies.Add(enemyObj);
    }
    // An event will be fired from the enemy health script which tells that hey my health is zero! , take me out from the enemies in the current scene queue and put me out in the deadEnemies queue

}