using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float shieldTime;
    private SpriteRenderer spriteRenderer;
    public bool isShield;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isShield = true;
    }
    void Start()
    {
        Invoke("Hide", shieldTime);
    }
    public void Hide()
    {
        spriteRenderer.enabled = false;
        this.isShield = false;
    }
    public void Show()
    {
        spriteRenderer.enabled = true;
        this.isShield = true;
        Start();
    }
}
