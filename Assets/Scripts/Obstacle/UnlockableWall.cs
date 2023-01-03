using UnityEngine;

public class UnlockableWall : MonoBehaviour, IUnlockable
{
    const string UNLOCK_SOUND_LABEL = "unlock";

    public AudioClip unlockSound;

    public void Unlock()
    {
        AudioManager.Instance.SetEffect(UNLOCK_SOUND_LABEL, unlockSound);
        AudioManager.Instance.PlayEffect(UNLOCK_SOUND_LABEL);
        Destroy(gameObject);
    }
}
