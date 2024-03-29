using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atomic : MonoBehaviour
{
    [SerializeField] private float dameBullet;
    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        transform.localScale = new Vector3 (width, height, 0);
    }

    private void Update()
    {
        //Destroy(gameObject, 0.05f);
        DestroyImmediate(gameObject);
    }

    public float getDameBullet()
    {
        return dameBullet;
    }
}
