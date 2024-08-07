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

    public void PlayerDie()
    {
        Invoke("MainScene", 3f);
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CityScene()
    {
        SceneManager.LoadScene("CityScene");
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
