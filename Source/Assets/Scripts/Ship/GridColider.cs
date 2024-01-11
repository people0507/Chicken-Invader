using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridColider : MonoBehaviour
{
    public int gridSizeX = 5;
    public int gridSizeY = 5;
    public float cellSize = 1f;

    void Start()
    {
        GenerateGridColliders();
    }

    void GenerateGridColliders()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0f);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = position;

                BoxCollider boxCollider = cube.AddComponent<BoxCollider>();
                boxCollider.size = new Vector3(cellSize, cellSize, 1f);
            }
        }
    }
}
