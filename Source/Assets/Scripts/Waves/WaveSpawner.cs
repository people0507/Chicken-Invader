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
    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime;

    private Vector3 posChicken;
    [SerializeField] private float grid = 2;

    private void Awake()
    {
        PosChicken();
    }
    // Update is called once per frame
    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();

        GameObject[] totalChickens = GameObject.FindGameObjectsWithTag("Chicken");
        GameObject[] totalRocks = GameObject.FindGameObjectsWithTag("Rock");

        GameObject[] totalEnemies = new GameObject[totalChickens.Length + totalRocks.Length];
        totalChickens.CopyTo(totalEnemies, 0);
        totalRocks.CopyTo(totalEnemies, 0);

        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length){
            currentWaveNumber++;
            canSpawn = true;
            PosChicken();
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime <Time.time)
        {
            GameObject enemy = currentWave.enemy;
            float x = Camera.main.ViewportToWorldPoint(Vector2.one).x;
            float y = Camera.main.ViewportToWorldPoint(Vector2.one).y;
            if (enemy.CompareTag("Rock"))
                Instantiate(enemy, new Vector3(Random.Range(-x/1.25f, x/1.25f) - x, y, 0), Quaternion.identity);
            else if (enemy.CompareTag("Chicken"))
            {
                Chicken chicken = Instantiate(enemy, new Vector3(Random.Range(-x / 2, x / 2), y, 0), Quaternion.identity).GetComponent<Chicken>();
                chicken.MoveToPos(posChicken.x, posChicken.y);
                posChicken.x -= grid;
                if(posChicken.x <= -x)
                {
                    posChicken.x = x-1;
                    posChicken.y-=grid;
                }
            }
            else if (enemy.CompareTag("BossChicken"))
            {
                ChickenBoss chickenBoss = Instantiate(enemy, new Vector3(0, y, 0), Quaternion.identity).GetComponent<ChickenBoss>();
                chickenBoss.MoveToPos(0, y - 2);
            }

            currentWave.numEnemy--;
            nextSpawnTime = Time.time + currentWave.timeSpawnEnemy;
            if(currentWave.numEnemy == 0)
            {
                canSpawn = false;
            }
        }
    }

    private void PosChicken()
    {
        float x = Camera.main.ViewportToWorldPoint(Vector2.one).x;
        float y = Camera.main.ViewportToWorldPoint(Vector2.one).y;
        this.posChicken = new Vector3(x - 1, y - 1);
    }

}
