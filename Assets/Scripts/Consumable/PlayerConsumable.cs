using UnityEngine;

/// <summary>
/// A consumable that affects the player.
/// </summary>
public abstract class PlayerConsumable : MonoBehaviour, IConsumable
{
    public PlayerController GetPlayer()
    {
        return GameManager.Instance.PlayerController;
    }
    public abstract void Consume();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player))
        {
            Destroy(gameObject);
        }
    }
}
