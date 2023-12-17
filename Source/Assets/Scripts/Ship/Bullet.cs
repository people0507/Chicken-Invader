using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private float yMax;
    private BoxCollider2D box;
    void Start()
    {
        yMax = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        OnDestroy();
    }
    private void OnDestroy()
    {
        if (transform.position.y > yMax)
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "RedChicken")
        {
            Destroy(gameObject);
        }
    }
}
