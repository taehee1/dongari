using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject[] hpUi;
    public Slider bossHp;

    public static UiManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(CoStartBossHp());
    }

    private void Update()
    {
        BossHpUpdate();
    }

    public void HpUiUpdate()
    {
        hpUi[Player.instance.currentHp].SetActive(false);
    }

    private void BossHpUpdate()
    {
        bossHp.value = 1 * (Boss.instance.hp / Boss.instance.maxHp);
    }

    private IEnumerator CoStartBossHp()
    {
        while (Boss.instance.hp < Boss.instance.maxHp)
        {
            yield return new WaitForSeconds(0.0001f);
            Boss.instance.hp++;

            if (Boss.instance.hp == Boss.instance.maxHp)
            {
                Player.instance.canMove = true;
            }
        }
    }
}
