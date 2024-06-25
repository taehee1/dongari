using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;
    private Collider2D collider2D;
    private float shrinkDuration = 1.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        collider2D = GetComponent<Collider2D>();
        Invoke("HitOn", 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void HitOn()
    {
        color = new Color(0, 1, 1, 1);
        spriteRenderer.color = color;
        collider2D.enabled = true;
        Invoke("StartShrinkCoroutine", 1f);
    }

    private void StartShrinkCoroutine()
    {
        StartCoroutine(ShrinkAndDisappear());
    }

    private IEnumerator ShrinkAndDisappear()
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = new Vector3(
                originalScale.x,
                originalScale.y * (1 - (elapsedTime / shrinkDuration)),
                originalScale.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(0, originalScale.y, originalScale.z);
        collider2D.enabled = false;
        Destroy(gameObject);
    }
}
