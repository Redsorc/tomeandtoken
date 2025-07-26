using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameEntry : MonoBehaviour
{
    public Button startDraftButton;
    public Button quitButton;
    public DialogueController dialogueController;
    public DraftUIController draftUIController;

    void Start()
    {
        UIManager.Instance.BindButton(startDraftButton, OnStartDraftClicked);
        UIManager.Instance.BindButton(quitButton, () => Application.Quit());
    }

    void OnStartDraftClicked()
    {
        // Intro dialogue lines
        List<string> intro = new List<string> {
            "You need to cram for the wizard duels tournament tomorrow.",
            "Memorize what spells you can!"
        };
        dialogueController.StartDialogue(intro, OnDialogueComplete);
        UIManager.Instance.ShowScreen(ScreenID.Dialogue);
    }

    void OnDialogueComplete()
    {
        UIManager.Instance.ShowScreen(ScreenID.Draft);
        draftUIController.BeginDraft();
    }
}