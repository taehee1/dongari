using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    public int jumpPower; //점프높이
    public float moveSpeed = 5f; // 이동 속도
    public AnimationClip[] clip;
    public static bool jumpcool = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Space 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcool == true)
            {
                rb.velocity = Vector2.up * jumpPower;
                jumpcool = false;
            }

        }

        // D 키를 누르는 동안 오른쪽으로 이동
        if (Input.GetKey(KeyCode.D))
        {
            // 캐릭터를 오른쪽으로 이동시키는 로직
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // A 키를 누르는 동안 왼쪽으로 이동
        if (Input.GetKey(KeyCode.A))
        {
            // 캐릭터를 오른쪽으로 이동시키는 로직
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
