using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float a, b;
    private float xPos, yPos;
    private bool positive = false;
    private float rotation = 0;
    [SerializeField] private float speed;
    [SerializeField] private GameObject egg;
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        this.b = Camera.main.ViewportToWorldPoint(Vector2.one).y - Random.Range(1, 3);
        xPos = transform.position.x;
        if (xPos > 0)
            positive = true;
        yPos = transform.position.y;
        this.a = (yPos - b) / Mathf.Pow(xPos, 2);
        StartCoroutine(Move());
        StartCoroutine(EnemyShoot());
    }

    private void Update()
    {
        float y = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        if (transform.position.y < y - 1)
            Destroy(gameObject);
    }

    private IEnumerator Move()
    {
        Vector3 point;
        if (positive)
        {
            xPos -= 1;
            point = new Vector3(xPos, CalculateY(xPos), 0);

            Vector3 boforePoint1 = new Vector3(xPos + 1, CalculateY(xPos + 1), 0);
            Vector3 boforePoint2 = new Vector3(xPos + 2, CalculateY(xPos + 2), 0);

            Vector3 beforRotate = boforePoint1 - boforePoint2;
            Vector3 afterRotate = point - boforePoint1;

            float angle = Vector3.Angle(beforRotate, afterRotate);

            transform.Rotate(0, 0, angle);
            rotation += angle;
        }
        else
        {
            xPos += 1;
            point = new Vector3(xPos, CalculateY(xPos), 0);

            Vector3 boforePoint1 = new Vector3(xPos - 1, CalculateY(xPos - 1), 0);
            Vector3 boforePoint2 = new Vector3(xPos - 2, CalculateY(xPos - 2), 0);

            Vector3 beforRotate = boforePoint1 - boforePoint2;
            Vector3 afterRotate = point - boforePoint1;

            float angle = Vector3.Angle(beforRotate, afterRotate);

            transform.Rotate(0, 0, -angle);
            rotation += angle;
        }
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(Move());
    }

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(positive)
                Instantiate(egg, transform.position - new Vector3(0, 0.6f, 0), Quaternion.Euler(0, 0, rotation));
            else
                Instantiate(egg, transform.position - new Vector3(0, 0.6f, 0), Quaternion.Euler(0, 0, -rotation));
            audioManager.PlayEgg(audioManager.eggClip);
        }
    }

    private float CalculateY(float x)
    {
        return a * Mathf.Pow(x, 2) + b;
    }
    private float CalculateXMin(float y)
    {
        return Mathf.Sqrt((y - b) / a);
    }
}
