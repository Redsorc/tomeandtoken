using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class DraftManager : MonoBehaviour
{
    public static DraftManager Instance { get; private set; }

    public event Action<List<Card>> OnPackReady;
    public event Action<List<Card>> OnDraftComplete;

    List<Card> rares, uncommons, commons;
    int pickCount;
    public List<Card> Deck { get; private set; }
    public List<Card> CurrentPack { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDraft()
    {
        // load and shuffle pools
        rares = CardDatabase.Instance.GetCardsByRarity(Rarity.Rare).OrderBy(_ => Guid.NewGuid()).ToList();
        uncommons = CardDatabase.Instance.GetCardsByRarity(Rarity.Uncommon).OrderBy(_ => Guid.NewGuid()).ToList();
        commons = CardDatabase.Instance.GetCardsByRarity(Rarity.Common).OrderBy(_ => Guid.NewGuid()).ToList();

        pickCount = 0;
        Deck = new List<Card>();
        NextPack();
    }

    void NextPack()
    {
        if (pickCount >= 30)
        {
            OnDraftComplete?.Invoke(Deck);
            return;
        }

        // Rare (take and remove first)
        var pack = new List<Card> { rares[0] }; rares.RemoveAt(0);

        // 3 Uncommons
        pack.AddRange(uncommons.Take(3));
        uncommons.RemoveRange(0, 3);

        // 6 Commons
        pack.AddRange(commons.Take(6));
        commons.RemoveRange(0, 6);

        // Shuffle pack
        CurrentPack = pack.OrderBy(_ => Guid.NewGuid()).ToList();

        OnPackReady?.Invoke(CurrentPack);
    }

    public void Select(int index)
    {
        var choice = CurrentPack[index];
        Deck.Add(choice);
        pickCount++;
        NextPack();
    }
}