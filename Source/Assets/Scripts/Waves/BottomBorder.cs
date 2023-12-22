using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
    private BoxCollider2D box;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        float yMin = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        box.transform.position = new Vector2(0, yMin);
        box.size = new Vector2(cameraWidth, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
