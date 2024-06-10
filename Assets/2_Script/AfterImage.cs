using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float fadeDuration = 0.5f; // 잔상이 사라지는데 걸리는 시간
    private SpriteRenderer spriteRenderer;
    private Color color;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        Destroy(gameObject, fadeDuration); // 일정 시간 후 잔상 객체 삭제
    }

    void Update()
    {
        float alpha = color.a - (Time.deltaTime / fadeDuration);
        color = new Color(color.r, color.g, color.b, alpha);
        spriteRenderer.color = color;
    }
}
