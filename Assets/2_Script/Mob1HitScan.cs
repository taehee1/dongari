using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1HitScan : MonoBehaviour
{
    public GameObject mob;
    private bool canAttack = true;
    public float attackTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canAttack == true)
        {
            mob.GetComponent<Mob>().Attack();
            canAttack = false;
        }
    }

    private void Update()
    {
        attackTimer();
    }

    private void attackTimer()
    {
        if (attackTime <= 0f)
        {
            canAttack = true;
            attackTime = 4f;
        }
        
        attackTime -= Time.deltaTime;
    }
}
