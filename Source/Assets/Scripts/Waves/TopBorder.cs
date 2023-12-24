using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBorder : MonoBehaviour
{
    private BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        float yMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y;
        box.transform.position = new Vector2(0, yMax);
        box.size = new Vector2(cameraWidth, 0.5f);
    }
}
