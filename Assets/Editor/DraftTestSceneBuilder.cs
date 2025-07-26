using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DraftTestSceneBuilder
{
    [MenuItem("Tools/Build Draft Test Scene")]
    public static void BuildScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        var manager = new GameObject("GameEntry").AddComponent<GameEntry>();
        var uiManager = new GameObject("UIManager").AddComponent<UIManager>();
        // Assume DraftUIController prefab exists
        var draftPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/DraftScreen.prefab");
        PrefabUtility.InstantiatePrefab(draftPrefab);
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/DraftTest.unity");
        Debug.Log("Draft test scene built.");
    }
}