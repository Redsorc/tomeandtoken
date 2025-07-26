using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerState
{
    public List<Card> Library;
    public List<Card> Hand;
    public List<Card> Battlefield;
    public List<Card> ManaPool;  // Cards sacrificed as mana
    public int Life = 20;
    public bool HasEndedMainPhase;
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public PlayerState Human, AI;
    public event Action OnGameStart;
    public event Action<bool> OnTurnStart; // true = human
    public event Action OnGameEnd;

    bool isHumanTurn;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartBattle(List<Card> humanDeck, List<Card> aiDeck)
    {
        // Initialize states
        Human = new PlayerState { Library = new List<Card>(humanDeck) };
        AI = new PlayerState { Library = new List<Card>(aiDeck) };
        ShuffleAndDrawOpeningHands();
        OnGameStart?.Invoke();
        isHumanTurn = true;
        StartCoroutine(GameLoop());
    }

    void ShuffleAndDrawOpeningHands()
    {
        Human.Library = Human.Library.OrderBy(_ => Guid.NewGuid()).ToList();
        AI.Library = AI.Library.OrderBy(_ => Guid.NewGuid()).ToList();
        Human.Hand = DrawCards(Human.Library, 7);
        AI.Hand = DrawCards(AI.Library, 7);
        PerformLondonMulligan(Human);
        PerformLondonMulligan(AI);
    }

    List<Card> DrawCards(List<Card> library, int count)
    {
        var draw = library.Take(count).ToList();
        library.RemoveRange(0, draw.Count);
        return draw;
    }

    void PerformLondonMulligan(PlayerState player)
    {
        // Placeholder: no mulligan for now, or simple: always keep 7
    }

    System.Collections.IEnumerator GameLoop()
    {
        while (true)
        {
            yield return Turn(isHumanTurn);
            if (CheckGameEnd()) break;
            isHumanTurn = !isHumanTurn;
        }
        OnGameEnd?.Invoke();
    }

    System.Collections.IEnumerator Turn(bool human)
    {
        var active = human ? Human : AI;
        // Untap/mana reset handled implicitly
        // Draw
        if (active.Library.Any()) active.Hand.Add(DrawCards(active.Library, 1)[0]);
        OnTurnStart?.Invoke(human);

        // Main Phase: wait for input or AI
        if (human)
        {
            // signal UI to enable action buttons
            yield return new WaitUntil(() => active.HasEndedMainPhase);
        }
        else
        {
            AIPlayer.Instance.TakeTurn(AI, Human);
            yield return null;
        }

        // Combat Phase
        ResolveCombat(active, human ? AI : Human);
        yield return new WaitForSeconds(0.5f);

        yield break;
    }

    void ResolveCombat(PlayerState attacker, PlayerState defender)
    {
        int damage = attacker.Battlefield.Count * 1;
        defender.Life -= damage;
    }

    bool CheckGameEnd()
    {
        if (Human.Life <= 0 || AI.Life <= 0) return true;
        return false;
    }
}