using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place1 : MonoBehaviour
{
    GameObject parentObject;
    private float cameraWidth;

    // Start is called before the first frame update
    void Start()
    {
    parentObject = GameObject.Find(transform.parent.name);
    cameraWidth = -Camera.main.orthographicSize * Camera.main.aspect /2f;
    Vector3 parentPosition = parentObject.transform.position;
    Vector3 newPosition = new Vector3(cameraWidth, parentPosition.y, 0);
    transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
