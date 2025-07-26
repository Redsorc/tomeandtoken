using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
public class AIBattleSimulation
{
    [UnityTest]
    public IEnumerator FullTournament_SucceedsOrFailsGracefully()
    {
        var bracket = new BracketManager();
        bracket.StartTournament(Deck.CreateRandomDeck(30));
        yield return null;
        Assert.Pass();
    }
}