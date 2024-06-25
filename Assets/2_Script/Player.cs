using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stat")]
    private float maxHp = 100;
    private float currentHp;
    private float attackDmg;

    [Header("�̵�")]
    public int jumpPower; // ���� ����
    public float speed = 5f; // �̵� �ӵ�
    public static bool isFacingRight = true;
    private float horizontal;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("�뽬")]
    public float dashPower = 100f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public float afterImageSpawnInterval = 0.1f;
    public GameObject afterImagePrefab;

    private bool canDash = true;
    private bool isDashing;
    private float afterImageTimer;

    [Header("����")]
    public GameObject hitScan;
    public GameObject skillHitScan;

    private bool isAttacking = false;

    [Header("ī�޶�ȿ��")]
    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // ī�޶� ����ũ ���� �ð�
    public float shakeMagnitude = 0.1f; // ī�޶� ����ũ ����

    [Header("�ִϸ��̼�")]
    public Animator animator;

    Rigidbody2D rb;

    private void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Flip();

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !isAttacking)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f && !isAttacking)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Q) && canDash && !isAttacking)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(Dash());
            }
        }

        Attack();
        StrongAttack();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Move()
    {
        if (!isAttacking)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetBool("Run", horizontal != 0);
        }
        else
        {
            horizontal = 0f;
            animator.SetBool("Run", false);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        afterImageTimer = 0f;
        float dashTimer = dashTime;

        while (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
            afterImageTimer -= Time.deltaTime;

            if (afterImageTimer <= 0f)
            {
                SpawnAfterImage();
                afterImageTimer = afterImageSpawnInterval;
            }

            yield return null;
        }

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void SpawnAfterImage()
    {
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer spriteRenderer = afterImage.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1; // �ܻ��� ������Ʈ �ڷ� ������
        afterImage.transform.localScale = transform.localScale;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            hitScan.GetComponent<Collider2D>().enabled = true;
            Invoke("AttackDone", 0.5f);
            animator.SetTrigger("Attack");
        }
    }

    private void StrongAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            isAttacking = true;
            skillHitScan.GetComponent<Collider2D>().enabled = true;
            Invoke("AttackDone", 1f);
        }
    }

    private void AttackDone()
    {
        isAttacking = false;
        hitScan.GetComponent<Collider2D>().enabled = false;
        skillHitScan.GetComponent<Collider2D>().enabled = false;
    }
}
