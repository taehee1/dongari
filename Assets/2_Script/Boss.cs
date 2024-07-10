using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Stat")]
    public float maxHp = 1000;
    public float currentHP = 1;
    private bool isStun = false;
    private bool isDie = false;

    [Header("카메라")]
    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // 카메라 쉐이크 지속 시간
    public float shakeMagnitude = 0.1f; // 카메라 쉐이크 강도

    [Header("패턴1")]
    public GameObject attack1Prefab; // 기둥 프리팹
    public List<Transform> spawnPoints; // 기둥이 소환될 위치들
    public int numberOfAttack1 = 4; // 생성할 기둥의 개수
    private int pattern1Count = 0;

    [Header("패턴2")]
    public GameObject attack2Prefab;
    public Transform spawnPosition;
    public int numberOfAttack2 = 1;

    [Header("패턴3")]
    public bool isPattern3Triggered = false;
    public GameObject attack3_1;
    public GameObject attack3_2;
    public GameObject attack3_3;
    public GameObject attack3_4;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform player;

    public static Boss instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void PatternRandom()
    {
        if (isDie == false && isStun == false)
        {
            // 패턴 3 조건 추가 및 수정
            if (currentHP <= 800 && currentHP >= 601 && isPattern3Triggered == false)
            {
                Debug.Log("Pattern 3 - HP range 600-800");
                isPattern3Triggered = true;
                Pattern_3();
                return;
            }
            if (currentHP <= 600 && currentHP >= 401 && isPattern3Triggered == true)
            {
                Debug.Log("Pattern 3 - HP range 400-600");
                isPattern3Triggered = false;
                Pattern_3();
                return;
            }
            if (currentHP <= 400 && currentHP >= 201 && isPattern3Triggered == false)
            {
                Debug.Log("Pattern 3 - HP range 200-400");
                isPattern3Triggered = true;
                Pattern_3();
                return;
            }
            if (currentHP <= 200 && currentHP >= 1 && isPattern3Triggered == true)
            {
                Debug.Log("Pattern 3 - HP range 1-200");
                isPattern3Triggered = false;
                Pattern_3();
                return;
            }

            int pattern = Random.Range(1, 3);
            if (pattern == 1)
            {
                Debug.Log("1");
                Pattern_1();
                pattern1Count++;
            }
            else if (pattern == 2 && pattern1Count >= 2)
            {
                Debug.Log("2");
                Pattern_2();
                pattern1Count = 0;
            }
            else
            {
                PatternRandom();
            }
        }
    }

    private void Pattern_1()
    {
        animator.SetTrigger("Attack1");

        List<Transform> selectedSpawnPoints = GetRandomSpawnPoints();

        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            Instantiate(attack1Prefab, spawnPoint.position, Quaternion.Euler(0, 0, 90));
        }

        if (isStun == false)
        {
            Invoke("PatternRandom", 6f);
        }
    }

    private List<Transform> GetRandomSpawnPoints()
    {
        List<Transform> selectedPoints = new List<Transform>();
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < numberOfAttack1; i++)
        {
            if (availablePoints.Count == 0) break;

            int randomIndex = Random.Range(0, availablePoints.Count);
            selectedPoints.Add(availablePoints[randomIndex]);
            availablePoints.RemoveAt(randomIndex);
        }

        return selectedPoints;
    }

    private void Pattern_2()
    {
        animator.SetTrigger("Attack2");

        for (int i = 0; i < numberOfAttack2; i++)
        {
            Instantiate(attack2Prefab, spawnPosition.position, Quaternion.identity);
        }
        Invoke("PatternRandom", 3f);
    }

    private void Pattern_3()
    {
        if (currentHP <= 800 && currentHP >= 601) // 800
        {
            attack3_1.SetActive(true);
        }
        if (currentHP <= 600 && currentHP >= 401) // 600
        {
            attack3_2.SetActive(true);
        }
        if (currentHP >= 201 && currentHP <= 400) // 400
        {
            Debug.Log("asdasd");
            attack3_3.SetActive(true);
        }
        if (currentHP >= 1 && currentHP <= 200) // 200
        {
            attack3_4.SetActive(true);
        }
        Invoke("PatternRandom", 8f);
    }

    public void Stun()
    {
        isStun = true;
        animator.SetTrigger("Stun");
        Invoke("StunOff", 6f);
    }

    public void StunOff()
    {
        isStun = false;
        animator.SetTrigger("StunOff");
        Invoke("PatternRandom", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "HitScan")
        {
            shakeMagnitude = 4f;
            spriteRenderer.color = new Color(1f, 0.8f, 0.8f);
            currentHP -= Player.instance.attackDmg;
            Player.instance.HitSound();
            DieCheck();
            Debug.Log($"Boss Hp : {currentHP}");
            //StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            Invoke("MobColorReset", 0.3f);
        }
        else if (collision.tag == "SkillHitScan")
        {
            shakeMagnitude = 15f;
            spriteRenderer.color = new Color(1f, 0.8f, 0.8f);
            //StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            Invoke("MobColorReset", 0.3f);
        }
    }

    public void DieCheck()
    {
        if (currentHP <= 0)
        {
            isDie = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Die");
            Invoke("Credit", 5f);
        }
    }

    private void Credit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    private void MobColorReset()
    {
        spriteRenderer.color = new Color(1, 1, 1);
    }
}
