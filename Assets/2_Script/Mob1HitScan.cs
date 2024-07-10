using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1HitScan : MonoBehaviour
{
    public GameObject mob;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mob.GetComponent<Mob>().Attack();
        }
    }
}
