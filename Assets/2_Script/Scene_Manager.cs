using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("Main");
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
