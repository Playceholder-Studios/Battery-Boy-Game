using UnityEngine;

/// <summary>
/// A consumable that heals the player a certain amount.
/// </summary>
public class Key : PlayerConsumable
{
    public override void Consume()
    {
        GameManager.GetPlayer().hasKey = true;
    }
}
