using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBound : MonoBehaviour
{
private float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));

        minX = -bounds.x;
        maxX = bounds.x;

        minY = -bounds.y;
        maxY = bounds.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;

        if(temp.x < minX) {
            temp.x = minX; }
        else if(temp.x > maxX)
        {
            temp.x = maxX;
        }

        if(temp.y < minY)
        {
            temp.y = minY;
        }
        else if(temp.y > maxY)
        {
            temp.y = maxY;
        }

        transform.position = temp;
    }

}
