using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.Tilemaps;
using UnityEngine;

public enum MoveOption
{
    None,
    MoveToPos,
    MoveToLemniscate
}
[System.Serializable]
public class Wave
{
    public string waveName;
    public int numEnemy;
    public GameObject enemy;
    public float hpEnemy;
    public float speedStone;
    public float timeSpawnEnemy;

    public MoveOption move;
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

    private AudioManager audioManager;
    private bool gameWin = true;

    private void Awake()
    {
        PosChicken();
        canvasWin = GameObject.Find("GameWin").GetComponent<Canvas>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
            {
                if (gameWin)
                {
                    audioManager.PlayBackground(audioManager.gameWinClip);
                    Invoke("ShowCanvas", timeShowCanvas);
                    gameWin = false;
                }
            }
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
            {
                Rock rock = Instantiate(enemy, new Vector3(Random.Range(-x / 1.25f, x * 2) - x, y, 0), Quaternion.identity).GetComponent<Rock>();
                rock.setSpeed(currentWave.speedStone);
            }
                
            else if (enemy.CompareTag("Chicken"))
            {
                GameObject instantiatedObject = Instantiate(enemy, new Vector3(Random.Range(-x / 2, x / 2), y, 0), Quaternion.identity);
                Chicken chicken = instantiatedObject.GetComponent<Chicken>();
                OnTrigger trigger = instantiatedObject.GetComponent<OnTrigger>();
                if(trigger.GetType().GetMethod("setHp") != null)
                    trigger.setHp(currentWave.hpEnemy);
                if (currentWave.move == MoveOption.MoveToLemniscate)
                {
                    chicken.setMoveLemniscate(0, 3);
                }
                
                if (currentWave.move == MoveOption.MoveToPos)
                {
                    chicken.setMoveToPos(posChicken.x, posChicken.y);
                    posChicken.x -= grid;
                    if (posChicken.x <= -x)
                    {
                        posChicken.x = x - 1;
                        posChicken.y -= grid;
                    }
                }
            }
            else if (enemy.CompareTag("BossChicken"))
            {
                GameObject gameObject = Instantiate(enemy, new Vector3(0, y, 0), Quaternion.identity);
                ChickenBoss enemyy = gameObject.GetComponent<ChickenBoss>();
                OnTrigger trigger = gameObject.GetComponent<OnTrigger>();
                if (trigger.GetType().GetMethod("setHp") != null)
                    trigger.setHp(currentWave.hpEnemy);
            }
            else if (enemy.CompareTag("BigEgg"))
            {
                GameObject gameObject = Instantiate(enemy, new Vector3(Random.Range(-x + 1, x - 1), y, 0), Quaternion.identity);
                BigEgg enemyy = gameObject.GetComponent<BigEgg>();
                OnTrigger trigger = gameObject.GetComponent<OnTrigger>();
                if (trigger.GetType().GetMethod("setHp") != null)
                    trigger.setHp(currentWave.hpEnemy);
            }
            else if (enemy.CompareTag("Rocket"))
            {
                GameObject gameObject = Instantiate(enemy, new Vector3(Random.Range(-x + 1, x - 1), -y, 0), Quaternion.identity);
                Rocket rocket = gameObject.GetComponent<Rocket>();
                OnTrigger trigger = gameObject.GetComponent<OnTrigger>();
                if (trigger.GetType().GetMethod("setHp") != null)
                    trigger.setHp(currentWave.hpEnemy);
            }
                
            currentWave.numEnemy--;
            nextSpawnTime = Time.time + currentWave.timeSpawnEnemy;
            if(currentWave.numEnemy <= 0)
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
        Cursor.visible = true;
        canvasWin.setActiveTrue();
        Ship ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        ship.setControl(false);
    }
}
