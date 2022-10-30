using UnityEngine;

public class KeyObstacleHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.hasKey = true;
        }
    }
}
