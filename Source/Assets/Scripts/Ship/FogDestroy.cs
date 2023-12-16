using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogDestroy : MonoBehaviour
{
    public float aliveTimeFog;
// Start is called before the first frame update
    void Start()
    {
    Destroy(gameObject,aliveTimeFog);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
