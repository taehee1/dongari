using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public DialogueSystem system;

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
