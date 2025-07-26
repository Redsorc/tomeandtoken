using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    private List<Card> cards;
    public Deck(IEnumerable<Card> initialCards)
    {
        cards = new List<Card>(initialCards);
    }

    public void Shuffle()
    {
        cards = cards.OrderBy(_ => Guid.NewGuid()).ToList();
    }

    public Card Draw()
    {
        if (cards.Count == 0) return null;
        Card top = cards[0];
        cards.RemoveAt(0);
        return top;
    }

    public void LondonMulligan(int keepCount)
    {
        Shuffle();
        var hand = cards.Take(keepCount).ToList();
        int toBottom = cards.Count - keepCount;
        var bottom = cards.Skip(keepCount).Take(toBottom).ToList();
        cards = hand.Concat(bottom).ToList();
    }

    // Helper to create a random deck of given size from database
    public static List<Card> CreateRandomDeck(int size)
    {
        return CardDatabase.Instance.AllCards
            .OrderBy(_ => Guid.NewGuid())
            .Take(size)
            .ToList();
    }
}