using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public static Mob Instance;

    SpriteRenderer spriteRenderer;

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

        if (collision.gameObject.tag == "HitScan")
        {
            spriteRenderer.color = new Color(1f, 0.8f, 0.8f);

            if (Player.isFacingRight == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(7f + Player.instance.attackDmg / 3, 7f + Player.instance.attackDmg / 6, 0);
            }
            else if (Player.isFacingRight == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-7f - Player.instance.attackDmg / 3, 7f + Player.instance.attackDmg / 6, 0);
            }
            mobHp -= Player.instance.attackDmg;
            animator.SetTrigger("Attacked");
            Player.instance.HitSound();
            Invoke("MobColorReset", 0.3f);
            DieCheck();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Player.instance.currentHp -= 1;
        UiManager.instance.HpUiUpdate();
        Player.instance.animator.SetTrigger("Hited");
        Player.instance.Stun();
        Player.instance.DieCheck();
        Player.instance.GodModeOn();
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
            transform.Translate(MoveSpeed*Time.deltaTime,0,0);
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
