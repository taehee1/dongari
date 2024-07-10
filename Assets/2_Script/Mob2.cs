using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2 : MonoBehaviour
{
    public static Mob Instance;

    SpriteRenderer spriteRenderer;

    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // 카메라 쉐이크 지속 시간
    public float shakeMagnitude = 0.1f; // 카메라 쉐이크 강도

    public float mobHp = 30;
    public float mobDmg = 10;

    private float moveTime = 0f;
    private float TurnTime = 0f;
    public float MoveSpeed = 0f;

    private Animator Mobaim;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Mobaim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(1f, 0.5f, 0.5f);
        Player.instance.HitSound();

        if (collision.tag == "HitScan")
        {
            shakeMagnitude = 4f;
            Mobaim.SetTrigger("Hit");
            mobHp -= Player.instance.attackDmg;

            Invoke("MobColorReset", 0.3f);
        }
        else if (collision.tag == "SkillHitScan")
        {
            shakeMagnitude = 15f;
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            Mobaim.SetTrigger("Hit");

            Invoke("MobColorReset", 0.3f);
        }
        //공격
        else if (collision.gameObject.tag == "Player")
        {
            Mobaim.SetTrigger("Attack");
        }
    }
    private void MobColorReset()
    {
        spriteRenderer.color = new Color(1, 1, 1);
    }

    void Update()
    {
        MonsterMove();
        DieCheck();
    }

    //몹 움직임
    private void MonsterMove()
    {
        moveTime += Time.deltaTime;
        if (moveTime <= TurnTime)
        {
            this.transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            TurnTime = Random.Range(1, 5);
            moveTime = 0;

            transform.Rotate(0, 180, 0);
        }
    }

    private void DieCheck()
    {
        if (mobHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
