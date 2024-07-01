using UnityEngine;

public class Attack2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    private Vector2 currentDirection;
    private bool isReversing = false; // 반전 상태를 추적하기 위한 변수

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDirection = (player.position - transform.position).normalized;
        Invoke("ObjDelete", 20f);
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!isReversing)
        {
            currentDirection = (player.position - transform.position).normalized;
        }

        if (currentDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameObject.tag == "Energyball")
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
        else if (collision.tag == "HitScan" && gameObject.tag == "Energyball")
        {
            ReverseDirection();
            gameObject.tag = "ReverseEnergyball";
        }

        if (collision.tag == "Boss" && gameObject.tag == "ReverseEnergyball")
        {
            Boss.instance.Stun();
            ObjDelete();
        }

        if (collision.tag == "Wall" && gameObject.tag == "ReverseEnergyball")
        {
            ObjDelete();
        }
    }

    private void ReverseDirection()
    {
        currentDirection = -currentDirection;
        isReversing = true;
    }

    private void ObjDelete()
    {
        Destroy(gameObject);
    }
}
