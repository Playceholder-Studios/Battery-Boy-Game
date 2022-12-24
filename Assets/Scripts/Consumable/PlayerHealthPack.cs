using UnityEngine;

/// <summary>
/// A consumable that heals the player a certain amount.
/// </summary>
public class PlayerHealthPack : PlayerConsumable
{
    public int HealAmount = 1;
    public override void Consume()
    {
        GetPlayer().HealPlayer(HealAmount);
    }
}
