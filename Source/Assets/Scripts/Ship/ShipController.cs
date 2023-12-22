using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    public static ShipController instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate( ship , transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnShip()
    {
        Instantiate(ship, transform);
    }
}
