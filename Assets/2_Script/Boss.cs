using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public CameraShake cameraShake;
    public float shakeDuration = 0.1f; // 카메라 쉐이크 지속 시간
    public float shakeMagnitude = 0.1f; // 카메라 쉐이크 강도

    public GameObject attack1Prefab; // 기둥 프리팹
    public List<Transform> spawnPoints; // 기둥이 소환될 위치들
    public int numberOfAttack1 = 4; // 생성할 기둥의 개수

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform player;

    private int chance = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PatternRandom();
    }

    private void PatternRandom()
    {
        chance++;
        Debug.Log($"Chance : {chance}");
        int pattern = Random.Range(1, 2);
        if (pattern == 1)
        {
            Debug.Log("1");
            Pattern_1();
        }
        else if (pattern == 2)
        {
            Debug.Log("2");
            Pattern_2();
        }
        else if (pattern == 3)
        {
            Debug.Log("3");
            Pattern_3();
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

        Invoke("PatternRandom", 5f);
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
        Invoke("PatternRandom", 3f);
    }

    private void Pattern_3()
    {
        Invoke("PatternRandom", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = new Color(1f, 0.8f, 0.8f);

        if (collision.tag == "HitScan")
        {
            shakeMagnitude = 4f;
            //StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            Invoke("MobColorReset", 0.3f);
        }
        else if (collision.tag == "SkillHitScan")
        {
            shakeMagnitude = 15f;
            //StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            Invoke("MobColorReset", 0.3f);
        }
    }

    private void MobColorReset()
    {
        spriteRenderer.color = new Color(1, 1, 1);
    }
}
