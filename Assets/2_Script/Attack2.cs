using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("ObjDelete", 8f);
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Player.instance.godMode == false)
            {
                Player.instance.currentHp -= 1;
                UiManager.instance.HpUiUpdate();
                Player.instance.animator.SetTrigger("Hited");
                Player.instance.Stun();
                Player.instance.DieCheck();
                Player.instance.GodModeOn();
            }

            Destroy(gameObject);
        }
    }

    private void ObjDelete()
    {
        Destroy(gameObject);
    }
}
