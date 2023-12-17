using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D myBody;
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    [SerializeField] private GameObject explosion;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 ở đây đại diện cho nút chuột trái
        {
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }
    void FixedUpdate()
    {
        ShipMovement();

    }
    void ShipMovement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        Vector3 temp = transform.position;
        temp.y += 0.6f;

        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, temp, Quaternion.identity);
        canShoot = true;
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "RedChicken" || target.tag == "Egg")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedChicken" || collision.gameObject.tag == "Egg")
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

}
