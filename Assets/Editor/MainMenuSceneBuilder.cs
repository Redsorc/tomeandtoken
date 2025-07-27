using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneBuilder
{
    [MenuItem("Tools/Build Main Menu Scene")]
    public static void BuildMainMenu()
    {
        // Create and save new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);

        // EventSystem
        new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem), typeof(UnityEngine.EventSystems.StandaloneInputModule));

        // Main Camera
        var camGO = new GameObject("Main Camera");
        var cam = camGO.AddComponent<Camera>();
        cam.tag = "MainCamera";
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.1f, 0.1f, 0.1f);

        // UIManager & GameEntry
        var managerGO = new GameObject("UIManager");
        var uiManager = managerGO.AddComponent<UIManager>();
        var gameEntry = managerGO.AddComponent<GameEntry>();

        // Canvas
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        canvasGO.AddComponent<GraphicRaycaster>();
        canvasGO.transform.SetParent(managerGO.transform, false);

        // Helper to create full-screen panel
        System.Action<string, string, bool> makePanel = (name, field, active) =>
        {
            var panel = new GameObject(name, typeof(Image));
            panel.transform.SetParent(canvasGO.transform, false);
            var rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            panel.GetComponent<Image>().color = new Color(0, 0, 0, 0.6f);
            panel.SetActive(active);
            // assign to UIManager
            typeof(UIManager).GetField(field).SetValue(uiManager, panel);
        };

        // Create screens
        makePanel("MainMenuScreen", "mainMenuScreen", true);
        makePanel("DialogueScreen", "dialogueScreen", false);
        makePanel("DraftScreen", "draftScreen", false);
        makePanel("BracketScreen", "bracketScreen", false);
        makePanel("BattleScreen", "battleScreen", false);

        // Style MainMenuScreen: Title + Buttons Layout
        var main = uiManager.mainMenuScreen;
        var layout = main.AddComponent<VerticalLayoutGroup>();
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.spacing = 20;
        layout.padding = new RectOffset(0, 0, 100, 100);

        // Title Text
        var titleGO = new GameObject("TitleText", typeof(Text));
        titleGO.transform.SetParent(main.transform, false);
        var title = titleGO.GetComponent<Text>();
        title.text = "Tome & Token";
        title.alignment = TextAnchor.MiddleCenter;
        title.fontSize = 72;
        title.color = Color.white;
        title.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // Button Factory
        System.Func<string, string, GameObject> makeButton = (name, label) =>
        {
            var go = new GameObject(name, typeof(Button), typeof(Image));
            go.transform.SetParent(main.transform, false);
            var img = go.GetComponent<Image>();
            img.color = new Color(0.2f, 0.2f, 0.2f);
            var btn = go.GetComponent<Button>();
            btn.targetGraphic = img;
            var txtGO = new GameObject("Text", typeof(Text));
            txtGO.transform.SetParent(go.transform, false);
            var txt = txtGO.GetComponent<Text>();
            txt.text = label;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.fontSize = 36;
            txt.color = Color.white;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            var rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(300, 80);
            return go;
        };

        // Create and bind buttons
        var startBtn = makeButton("StartDraftButton", "Start Draft");
        uiManager.BindButton(startBtn.GetComponent<Button>(), gameEntry.OnStartDraftClicked);
        gameEntry.startDraftButton = startBtn.GetComponent<Button>();

        var quitBtn = makeButton("QuitButton", "Quit");
        uiManager.BindButton(quitBtn.GetComponent<Button>(), () => Application.Quit());
        gameEntry.quitButton = quitBtn.GetComponent<Button>();

        // Save scene
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainMenu.unity");
        Debug.Log("Styled MainMenu scene built.");
    }
}