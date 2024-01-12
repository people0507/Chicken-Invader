using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootBossUFO : MonoBehaviour
{
    private enum MoveOptionBoss
    {
        SpawnEgg,
        SpawnBigEgg
    }

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject bigEgg;
    [SerializeField] private MoveOptionBoss moveOption;
    private Ship ship;
    private Quaternion targetRotation;

    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Start()
    {
        StartCoroutine(EnemyShoot());
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
    }

    void Update()
    {
        if (ship != null)
        {
            // Lấy vị trí của tàu
            Vector3 shipPos = ship.transform.position;

            // Tính toán hướng từ game object đến tàu
            Vector3 targetDirection = shipPos - transform.position;
            // Xoay game object chỉ xoay trục Z
            targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

            // Áp dụng xoay cho game object
            transform.rotation = targetRotation * Quaternion.Euler(0, 0, 140);
        }
    }
    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if(moveOption == MoveOptionBoss.SpawnEgg)
                Instantiate(egg, transform.position - new Vector3(0, -0.6f, 0), targetRotation * Quaternion.Euler(0, 0, 180));
            else if(moveOption == MoveOptionBoss.SpawnBigEgg)
                Instantiate(bigEgg, transform.position - new Vector3(0, -0.6f, 0), targetRotation * Quaternion.Euler(0, 0, 180));
            audioManager.PlayEgg(audioManager.eggClip);
        }
    }
}