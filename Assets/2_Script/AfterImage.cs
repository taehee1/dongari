using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float fadeDuration = 0.5f; // �ܻ��� ������µ� �ɸ��� �ð�
    private SpriteRenderer spriteRenderer;
    private Color color;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        Destroy(gameObject, fadeDuration); // ���� �ð� �� �ܻ� ��ü ����
    }

    void Update()
    {
        float alpha = color.a - (Time.deltaTime / fadeDuration);
        color = new Color(color.r, color.g, color.b, alpha);
        spriteRenderer.color = color;
    }
}
