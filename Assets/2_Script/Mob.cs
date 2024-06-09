using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float mobHp = 30;
    public float mobDmg = 10;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitScan")
        {
            spriteRenderer.color = new Color(1f, 0.8f, 0.8f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(5f, 5f, 0);
        }
    }
}
