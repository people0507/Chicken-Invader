using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float aliveTimeExplosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,aliveTimeExplosion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
