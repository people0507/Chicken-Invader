using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Khoảng thời gian giữa các nhấp nháy

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartBlinking();
    }

    private void OnDisable()
    {
        // Đảm bảo dừng coroutine khi GameObject bị tắt
        StopBlinking();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(Blink());
            spriteRenderer.enabled = true; // Đảm bảo đối tượng được hiển thị khi dừng nhấp nháy
        }
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0f; // Thời gian đã trôi qua
        while (isBlinking && elapsedTime < 3f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        spriteRenderer.enabled = true; // Đảm bảo đối tượng được hiển thị khi kết thúc nhấp nháy
    }
}
