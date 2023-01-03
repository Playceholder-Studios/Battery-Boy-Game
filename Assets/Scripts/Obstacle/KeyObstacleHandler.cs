using UnityEngine;

public class KeyObstacleHandler : MonoBehaviour
{
    const string PICKUP_SOUND_LABEL = "pickup";
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            AudioManager.Instance.SetEffect(PICKUP_SOUND_LABEL, pickupSound);
            AudioManager.Instance.PlayEffect(PICKUP_SOUND_LABEL);
            playerController.hasKey = true;
        }
    }
}
