using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;
    private Collider2D collider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        collider2D = GetComponent<Collider2D>();
        Invoke("A", 2f);
    }

    private void A()
    {
        color = new Color(0, 1, 1, 1);
        spriteRenderer.color = color;
        collider2D.enabled = true;
    }
}
