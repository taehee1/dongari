using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    

    public void BossScene()
    {
        SceneManager.LoadScene("BossScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
