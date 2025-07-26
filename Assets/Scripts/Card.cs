using UnityEngine;

public enum Rarity { Common, Uncommon, Rare }

[CreateAssetMenu(menuName = "TCG/Card")]
public class Card : ScriptableObject
{
    public string Id;
    public string Name;
    public Rarity Rarity;
    public Sprite Art;
    // Placeholder: all cards 1/1 for now
    public int Power => 1;
    public int Toughness => 1;
    // Future: ability definitions
}