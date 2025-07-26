using UnityEngine;
using System.Linq;

public class AIPlayer : MonoBehaviour
{
    public static AIPlayer Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TakeTurn(PlayerState self, PlayerState opponent)
    {
        // 1. Play a card as mana if available
        if (self.Hand.Count > 0)
        {
            var manaCard = self.Hand[0];
            self.Hand.RemoveAt(0);
            self.ManaPool.Add(manaCard);
        }
        // 2. Cast one creature if possible
        int availableMana = self.ManaPool.Count;
        var creature = self.Hand.FirstOrDefault();
        if (creature != null && availableMana >= 1)
        {
            self.Hand.Remove(creature);
            self.ManaPool.RemoveAt(0);
            self.Battlefield.Add(creature);
        }
        // End main phase immediately
        self.HasEndedMainPhase = true;
    }
}