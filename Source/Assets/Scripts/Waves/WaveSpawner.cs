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

    private Canvas canvasWin;
    [SerializeField] private float timeShowCanvas;

    private void Awake()
    {
        PosChicken();
        canvasWin = GameObject.Find("GameWin").GetComponent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();

        GameObject[] totalChickens = GameObject.FindGameObjectsWithTag("Chicken");
        GameObject[] totalRocks = GameObject.FindGameObjectsWithTag("Rock");
        GameObject[] totalBigEggs = GameObject.FindGameObjectsWithTag("BigEgg");
        GameObject[] totalBoss = GameObject.FindGameObjectsWithTag("BossChicken");
        GameObject[] totalRocket = GameObject.FindGameObjectsWithTag("Rocket");

        GameObject[] totalEnemies = new GameObject[totalChickens.Length + totalRocks.Length + totalBigEggs.Length + totalBoss.Length + totalRocket.Length];
        totalChickens.CopyTo(totalEnemies, 0);
        totalRocks.CopyTo(totalEnemies, 0);
        totalBigEggs.CopyTo(totalEnemies, 0);
        totalBoss.CopyTo(totalEnemies, 0);
        totalRocket.CopyTo(totalEnemies, 0);

        if (totalEnemies.Length == 0 && !canSpawn){
            if (currentWaveNumber + 1 != waves.Length)
            {
                currentWaveNumber++;
                canSpawn = true;
                PosChicken();
            }
            else
                Invoke("ShowCanvas", timeShowCanvas);
        }
    }

    void SpawnWave()
    {
        float x = Camera.main.ViewportToWorldPoint(Vector2.one).x;
        float y = Camera.main.ViewportToWorldPoint(Vector2.one).y;
        if (canSpawn && nextSpawnTime <Time.time)
        {
            GameObject enemy = currentWave.enemy;
            if (enemy.CompareTag("Rock"))
                Instantiate(enemy, new Vector3(Random.Range(-x / 1.25f, x * 2) - x, y, 0), Quaternion.identity);
            else if (enemy.CompareTag("Chicken"))
            {
                Chicken chicken = Instantiate(enemy, new Vector3(Random.Range(-x / 2, x / 2), y, 0), Quaternion.identity).GetComponent<Chicken>();
                chicken.MoveToPos(posChicken.x, posChicken.y);
                posChicken.x -= grid;
                if (posChicken.x <= -x)
                {
                    posChicken.x = x - 1;
                    posChicken.y -= grid;
                }
            }
            else if (enemy.CompareTag("BossChicken"))
            {
                Instantiate(enemy, new Vector3(0, y, 0), Quaternion.identity).GetComponent<ChickenBoss>();
            }
            else if (enemy.CompareTag("BigEgg"))
            {
                Instantiate(enemy, new Vector3(Random.Range(-x + 1, x - 1), y, 0), Quaternion.identity).GetComponent<BigEgg>();
            }
            else
                Instantiate(enemy, new Vector3(Random.Range(-x + 1, x - 1), -y, 0), Quaternion.identity);
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

    private void ShowCanvas()
    {
        canvasWin.setActiveTrue();
        Ship ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        ship.setControl(false);
    }
}
