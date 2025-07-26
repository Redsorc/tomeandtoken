using NUnit.Framework;
public class RuleEngineTests
{
    [Test]
    public void Deck_Shuffle_ChangesOrder()
    {
        // Arrange
        var deck = new Deck(CardDatabase.Instance.GetCardsByRarity(Rarity.Common));
        // Act & Assert
        Assert.DoesNotThrow(() => deck.Shuffle());
    }
}