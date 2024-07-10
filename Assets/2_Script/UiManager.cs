using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject[] hpUi;
    public GameObject[] Sounds;

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
                SoundPause();
            }
            else if (menuWorking == true)
            {
                menuWorking = false;
                menu.SetActive(false);
                Time.timeScale = 1f;
                SoundPlay();
            }
        }
    }

    public void Continue()
    {
        menuWorking = false;
        menu.SetActive(false);
        Time.timeScale = 1f;
        SoundPlay();
    }

    public void Title()
    {
        Time.timeScale = 1f;
        SoundPlay();
        SceneManager.LoadScene("StartScene");
    }

    private void SoundPause()
    {
        Sounds[0].GetComponent<AudioSource>().Pause();
        Sounds[1].GetComponent<AudioSource>().Pause();
    }

    private void SoundPlay()
    {
        Sounds[0].GetComponent<AudioSource>().Play();
        Sounds[1].GetComponent<AudioSource>().Play();
    }

    public void HpUiUpdate()
    {
        hpUi[Player.instance.currentHp].SetActive(false);
    }
}
