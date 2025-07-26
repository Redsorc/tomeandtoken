using System.Collections.Generic;
public static class MockDeckGenerator
{
    public static List<Card> GetCommonDeck(int size)
    {
        return new List<Card>(new Card[size]);
    }
}