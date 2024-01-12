using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject atomic;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float timeSpawnSmoke;
    [SerializeField] private float timeExitSmoke;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSmoke());
        float angle = Vector3.Angle(Vector3.up, Vector3.up - transform.position);
        if (transform.position.x > 0)
            transform.Rotate(0, 0, angle);
        else
            transform.Rotate(0, 0, -angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed*Time.deltaTime);
        if(transform.position == Vector3.zero)
        {
            Instantiate(atomic, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private IEnumerator SpawnSmoke()
    {
        while (true)
        {
            Vector3 direction = (Vector3.zero - transform.position).normalized;
            GameObject smokeObject = Instantiate(smoke, transform.position - direction, Quaternion.identity);
            Destroy(smokeObject, timeExitSmoke);
            yield return new WaitForSeconds(timeSpawnSmoke);
        }
    }
    private void OnDestroy()
    {
        
        var exp = Instantiate(explosion, transform.position, Quaternion.identity);  
        Destroy(exp, 0.5f);
    }
}
