using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject[] hpUi;
    public Slider bossHp;

    public GameObject menu;
    public bool menuWorking = false;

    public static UiManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        BossHpUpdate();
        Menu();
    }

    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuWorking == false)
            {
                menuWorking = true;
                menu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (menuWorking == true)
            {
                menuWorking = false;
                menu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Continue()
    {
        menuWorking = false;
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HpUiUpdate()
    {
        hpUi[Player.instance.currentHp].SetActive(false);
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
            yield return new WaitForSeconds(0.0001f);
            Boss.instance.currentHP++;

            if (Boss.instance.currentHP == Boss.instance.maxHp)
            {
                Player.instance.canMove = true;
                Boss.instance.PatternRandom();
            }
        }
    }
}
