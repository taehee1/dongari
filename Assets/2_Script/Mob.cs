using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // 카메라 쉐이크 지속 시간
    public float shakeMagnitude = 0.1f; // 카메라 쉐이크 강도

    public float mobHp = 30;
    public float mobDmg = 10;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(1f, 0.8f, 0.8f);

        if (collision.tag == "HitScan")
        {
            shakeMagnitude = 2f;
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));

            if (Player.isFacingRight == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(7f, 7f, 0);
            }
            else if (Player.isFacingRight == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-7f, 7f, 0);
            }
            Invoke("MobColorReset", 0.3f);
        }
        else if (collision.tag == "SkillHitScan")
        {
            shakeMagnitude = 15f;
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));

            if (Player.isFacingRight == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(15f, 15f, 0);
            }
            else if (Player.isFacingRight == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-15f, 15f, 0);
            }
            Invoke("MobColorReset", 0.3f);
        }
    }

    private void MobColorReset()
    {
        spriteRenderer.color = new Color(1, 1, 1);
    }
}
