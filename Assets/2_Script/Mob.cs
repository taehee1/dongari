using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public static Mob Instance;

    SpriteRenderer spriteRenderer;

    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // 카메라 쉐이크 지속 시간
    public float shakeMagnitude = 0.1f; // 카메라 쉐이크 강도

    public float mobHp = 30;
    public float mobDmg = 1;

    private float moveTime = 0f;
    private float TurnTime = 0f;
    public float MoveSpeed = 0f;

    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(1f, 0.8f, 0.8f);

        if (collision.tag == "HitScan")
        {
            shakeMagnitude = 4f;

            if (Player.isFacingRight == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(7f + Player.instance.attackDmg / 3, 7f + Player.instance.attackDmg / 6, 0);
            }
            else if (Player.isFacingRight == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-7f - Player.instance.attackDmg / 3, 7f + Player.instance.attackDmg / 6, 0);
            }
            mobHp -= Player.instance.attackDmg;
            Player.instance.HitSound();
            Invoke("MobColorReset", 0.3f);
            DieCheck();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void MobColorReset()
    {
        spriteRenderer.color = new Color(1, 1, 1);
    }

    void Update()
    {
        MonsterMove();
    }

    private void MonsterMove()
    {
        moveTime += Time.deltaTime;
        if(moveTime <= TurnTime) 
        {
            this.transform.Translate(MoveSpeed*Time.deltaTime,0,0);
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
