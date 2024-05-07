using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animation;
    public int jumpPower; //��������
    public float moveSpeed = 5f; // �̵� �ӵ�
    public AnimationClip[] clip;
    public static int jumpcool = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Space Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcool >= 1)
            {
                rb.velocity = Vector2.up * jumpPower;
                jumpcool--;
            }

        }

        // D Ű�� ������ ���� ���������� �̵�
        if (Input.GetKey(KeyCode.D))
        {
            // ĳ���͸� ���������� �̵���Ű�� ����
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // A Ű�� ������ ���� �������� �̵�
        if (Input.GetKey(KeyCode.A))
        {
            // ĳ���͸� ���������� �̵���Ű�� ����
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
