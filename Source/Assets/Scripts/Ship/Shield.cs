using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float shieldTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, shieldTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
