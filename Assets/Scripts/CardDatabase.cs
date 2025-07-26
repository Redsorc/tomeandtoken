using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance { get; private set; }
    public List<Card> AllCards { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCards();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadCards()
    {
        // Assumes Card assets are in Resources/CardData folder
        AllCards = Resources.LoadAll<Card>("CardData").ToList();
        Debug.Log($"Loaded {AllCards.Count} cards into database.");
    }

    /// <summary>
    /// Returns a filtered list of cards by the given rarity.
    /// </summary>
    public List<Card> GetCardsByRarity(Rarity rarity)
    {
        return AllCards.Where(c => c.Rarity == rarity).ToList();
    }
}