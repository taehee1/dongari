using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    public GameObject MenuPanel;
    void Start()
    {
       
    }

    void Update()
    {
       
    }
    public void MeunBtn()
    {
        MenuPanel.SetActive(true);
    }
    public void ContinueBth()
    {
        MenuPanel.SetActive(false);
    }
    public void titleBth()
    {
        SceneManager.LoadScene("StartScene");
    }
}
