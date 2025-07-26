using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class CardDefinition { public string Id, Name, Rarity, SpritePath; }
[Serializable]
public class CardDefinitionList { public List<CardDefinition> items; }

public class CardDatabaseGenerator
{
    const string jsonPath = "Assets/CardData/cards.json";
    const string assetFolder = "Assets/CardData";

    [MenuItem("Tools/Generate Card Database")]
    public static void GenerateDatabase()
    {
        if (!Directory.Exists(assetFolder)) Directory.CreateDirectory(assetFolder);
        string json = File.ReadAllText(jsonPath);
        var wrapper = JsonUtility.FromJson<CardDefinitionList>($"{{\"items\":{json}}}");
        foreach (var def in wrapper.items)
        {
            string assetPath = Path.Combine(assetFolder, def.Id + ".asset");
            var existing = AssetDatabase.LoadAssetAtPath<Card>(assetPath);
            Card so = existing != null ? existing : ScriptableObject.CreateInstance<Card>();
            so.Id = def.Id;
            so.Name = def.Name;
            so.Rarity = (Rarity)Enum.Parse(typeof(Rarity), def.Rarity);
            so.Art = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Art/{def.SpritePath}.png");
            if (existing == null) AssetDatabase.CreateAsset(so, assetPath);
            else EditorUtility.SetDirty(so);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Generated/Updated {wrapper.items.Count} Card assets.");
    }
}