using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private float gridSize = 2;
    private Vector3 SpawnPos;

    [SerializeField] private GameObject chickenPrefab;
    private void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width/Screen.height;

        SpawnPos = Camera.main.ScreenToViewportPoint(new Vector3(0, Screen.height, 0));
        SpawnPos.x += ((gridSize / 2 + (width / 4)));
        SpawnPos.y -= gridSize;
        SpawnPos.z = 0;
        SpawnChicken(Mathf.FloorToInt(height / 2 / gridSize), Mathf.FloorToInt(width / gridSize / 1.5f));
    }

    void SpawnChicken(int row, int number)
    {
        float x = SpawnPos.x;
        for(int i=0; i<row; i++)
        {
            for(int j=0; j<number; j++)
            {
                SpawnPos.x -= gridSize;
                GameObject chicken =  Instantiate(chickenPrefab, SpawnPos, Quaternion.identity);
            }
            SpawnPos.x = x;
            SpawnPos.y -= gridSize;
        }
    }
}
