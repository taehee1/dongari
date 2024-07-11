using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public CityDialogue system;

    private void Start()
    {
        Trigger();
    }

    public void Trigger()
    {
        Debug.Log("a");
        system.Begin(info);
        Debug.Log("b");
    }
}
