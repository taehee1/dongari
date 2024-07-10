using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Slider bossHp;

    public static BossUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        BossHpUpdate();
    }

    private void BossHpUpdate()
    {
        bossHp.value = 1 * (Boss.instance.currentHP / Boss.instance.maxHp);
    }

    public void StartBoss()
    {
        StartCoroutine(CoStartBossHp());
    }

    public IEnumerator CoStartBossHp()
    {
        while (Boss.instance.currentHP < Boss.instance.maxHp)
        {
            yield return new WaitForSeconds(0.001f);
            Boss.instance.currentHP++;

            if (Boss.instance.currentHP == Boss.instance.maxHp)
            {
                Player.instance.canMove = true;
                Boss.instance.PatternRandom();
            }
        }
    }
}
