using UnityEngine;

/// <summary>
/// Manages consumable interactions that affect the player.
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class PlayerConsumableManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Consumable))
        {
            collision.GetComponent<IConsumable>().Consume();
        }
    }
}
