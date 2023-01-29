using System;
using UnityEngine;

public class TriggeredWall : Triggerable, IUnlockable
{
    const string UNLOCK_SOUND_LABEL = "unlock";

    public AudioClip unlockSound;

    private void Start()
    {
        AudioManager.Instance.SetEffect(UNLOCK_SOUND_LABEL, unlockSound);
        base.triggerEvent += Unlock;
    }

    public void Unlock()
    {
        AudioManager.Instance.PlayEffect(UNLOCK_SOUND_LABEL);
        Destroy(gameObject);
    }
}
