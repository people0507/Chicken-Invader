using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int numEnemy;
    public GameObject enemy;
    public float timeSpawnEnemy;
}
public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPlace;
    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("RedChicken");
        if(totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length){
            currentWaveNumber++;
            canSpawn = true;
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime <Time.time) { 
        GameObject enemy = currentWave.enemy;
        Transform randomPlaceSpawn = spawnPlace[Random.Range(0, spawnPlace.Length)];
        Instantiate(enemy, randomPlaceSpawn.position,Quaternion.identity);
            currentWave.numEnemy--;
            nextSpawnTime = Time.time + currentWave.timeSpawnEnemy;
        if(currentWave.numEnemy == 0)
            {
                canSpawn = false;
            }
        }
    }
}
