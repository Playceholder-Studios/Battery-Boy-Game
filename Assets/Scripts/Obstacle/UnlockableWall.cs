using UnityEngine;

public class UnlockableWall : SceneObject, IUnlockable
{
    const string LOCKED_SOUND_LABEL = "locked";
    const string UNLOCK_SOUND_LABEL = "unlock";

    public AudioClip lockedSound;
    public AudioClip unlockSound;

    private void Start()
    {
        AudioManager.Instance.SetEffect(LOCKED_SOUND_LABEL, lockedSound);
        AudioManager.Instance.SetEffect(UNLOCK_SOUND_LABEL, unlockSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            PlayerController player = GameManager.GetPlayer();
            if (player.hasKey)
            {
                Unlock();
                player.keyHolder.SetActive(false);
                player.hasKey = false;
            }
            else {
                AudioManager.Instance.PlayEffect(LOCKED_SOUND_LABEL);
            }
        }
    }

    public void Unlock()
    {
        AudioManager.Instance.PlayEffect(UNLOCK_SOUND_LABEL);
        Destroy(gameObject);
    }
}
