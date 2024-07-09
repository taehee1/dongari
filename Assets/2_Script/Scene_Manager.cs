using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BossScene()
    {
        SceneManager.LoadScene("BossScene");
    }

    public void TilteScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
