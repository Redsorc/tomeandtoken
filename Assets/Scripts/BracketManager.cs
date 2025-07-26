using UnityEngine;
using System;
using System.Collections.Generic;

public class BracketManager : MonoBehaviour
{
    public static BracketManager Instance { get; private set; }
    public event Action<int> OnMatchStart; // match index or opponent ID
    public event Action<int, bool> OnMatchComplete; // match index, human won?
    public event Action<bool> OnTournamentEnd; // true = human champion

    private Queue<int> opponents;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CreateBracket(int playerCount = 32)
    {
        opponents = new Queue<int>(GenerateOpponents(playerCount));
    }

    List<int> GenerateOpponents(int count)
    {
        var list = new List<int>();
        for (int i = 1; i < count; i++) list.Add(i);
        return list;
    }

    public void StartTournament(List<Card> playerDeck)
    {
        CreateBracket();
        PlayNextMatch(playerDeck);
    }

    void PlayNextMatch(List<Card> playerDeck)
    {
        if (opponents.Count == 0)
        {
            OnTournamentEnd?.Invoke(true);
            return;
        }
        int opponentId = opponents.Dequeue();
        OnMatchStart?.Invoke(opponentId);

        // Create AI deck
        var aiDeck = Deck.CreateRandomDeck(30);

        // Start battle
        BattleManager.Instance.StartBattle(playerDeck, aiDeck);
        BattleManager.Instance.OnGameEnd += () =>
        {
            bool humanWon = BattleManager.Instance.Human.Life > 0;
            OnMatchComplete?.Invoke(opponentId, humanWon);
            if (humanWon) PlayNextMatch(playerDeck);
            else OnTournamentEnd?.Invoke(false);
        };
    }
}