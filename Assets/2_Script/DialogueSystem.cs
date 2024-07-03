using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text sentence;

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialogue info)
    {
        sentences.Clear();

        Debug.Log("aaaaaaaaa");

        foreach (var sentence in info.text)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }

        sentence.text = sentences.Dequeue();
    }

    private void End()
    {
        BossUI.instance.StartBoss();
        gameObject.SetActive(false);
    }
}
