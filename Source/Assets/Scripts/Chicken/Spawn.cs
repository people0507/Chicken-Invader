using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private BoxCollider2D box;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnerEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnerEnemy()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        float minX = -box.bounds.size.x / 2f;
        float maxX = box.bounds.size.x / 2f;

        Vector3 temp = transform.position;
        temp.x = Random.Range(minX, maxX);
        temp.y = transform.position.y;
        Instantiate(enemy, temp, Quaternion.identity);

        StartCoroutine(SpawnerEnemy());
    }
    //private void Spawn()
    //{
    //    GameObject chicken = Instantiate(this.enemy);
    //}
}
