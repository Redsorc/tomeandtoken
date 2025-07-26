using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    public Text dialogueText;
    public Button continueButton;
    private Action onComplete;
    private Queue<string> lines;

    public void StartDialogue(List<string> dialogueLines, Action callback)
    {
        lines = new Queue<string>(dialogueLines);
        onComplete = callback;
        ShowNextLine();
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(ShowNextLine);
    }

    void ShowNextLine()
    {
        if (lines.Count == 0)
        {
            onComplete?.Invoke();
            return;
        }
        dialogueText.text = lines.Dequeue();
    }
}