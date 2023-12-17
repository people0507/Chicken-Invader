using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreak : MonoBehaviour
{
    public float aliveTimeEggBreak;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,aliveTimeEggBreak);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
