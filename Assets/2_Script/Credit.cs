using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public GameObject returnBtn;
    public void ReturnBtn()
    {
        SceneManager.LoadScene("StartScene");
    }

    void Update()
    {
        
    }
}
