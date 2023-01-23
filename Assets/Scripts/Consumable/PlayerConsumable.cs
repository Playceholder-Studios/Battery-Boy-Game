using UnityEngine;

/// <summary>
/// A consumable that affects the player.
/// </summary>
public abstract class PlayerConsumable : SceneObject, IConsumable
{
    const string PICKUP_SOUND_LABEL = "pickup";
    const string DROP_SOUND_LABEL = "drop";
    public AudioClip pickupSound;
    public AudioClip dropSound;

    public abstract void Consume();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            AudioManager.Instance.SetEffect(PICKUP_SOUND_LABEL, pickupSound);
            AudioManager.Instance.PlayEffect(PICKUP_SOUND_LABEL);
            Consume();
            Destroy(gameObject);
        }
    }

    public virtual void Spawn(Vector3 position)
    {
        AudioManager.Instance.SetEffect(DROP_SOUND_LABEL, dropSound);
        AudioManager.Instance.PlayEffect(DROP_SOUND_LABEL);
        Instantiate(this, position, Quaternion.identity);
    }
}
