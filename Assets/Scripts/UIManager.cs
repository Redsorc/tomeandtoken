using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ScreenID
{
    MainMenu,
    Dialogue,
    Draft,
    Bracket,
    Battle
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Screen Roots")]
    public GameObject mainMenuScreen;
    public GameObject dialogueScreen;
    public GameObject draftScreen;
    public GameObject bracketScreen;
    public GameObject battleScreen;

    private Dictionary<ScreenID, GameObject> screens;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeScreens();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeScreens()
    {
        screens = new Dictionary<ScreenID, GameObject> {
            { ScreenID.MainMenu, mainMenuScreen },
            { ScreenID.Dialogue, dialogueScreen },
            { ScreenID.Draft, draftScreen },
            { ScreenID.Bracket, bracketScreen },
            { ScreenID.Battle, battleScreen }
        };

        // Hide all except MainMenu by default
        foreach (var kvp in screens)
        {
            kvp.Value.SetActive(kvp.Key == ScreenID.MainMenu);
        }
    }

    public void ShowScreen(ScreenID id)
    {
        foreach (var kvp in screens)
        {
            kvp.Value.SetActive(kvp.Key == id);
        }
    }

    public void BindButton(Button button, UnityEngine.Events.UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}