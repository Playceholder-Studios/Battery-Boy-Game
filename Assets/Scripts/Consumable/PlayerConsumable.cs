using UnityEngine;

/// <summary>
/// A consumable that affects the player.
/// </summary>
public abstract class PlayerConsumable : MonoBehaviour, IConsumable
{
    const string PICKUP_SOUND_LABEL = "pickup";
    public AudioClip pickupSound;

    public abstract void Consume();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            AudioManager.Instance.SetEffect(PICKUP_SOUND_LABEL, pickupSound);
            AudioManager.Instance.PlayEffect(PICKUP_SOUND_LABEL);
            Destroy(gameObject);
        }
    }
}
