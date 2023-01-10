using UnityEngine;

public class FireRateUpgrade : PlayerConsumable
{
    const string POWER_UP_MUSIC_LABEL = "powerUpSound";

    /// <summary>
    /// 
    /// </summary>
    public float fireRate = 0.5f;

    /// <summary>
    /// The length that the upgrade lasts
    /// </summary>
    public float duration = 2f;

    private Timer timer;

    public AudioClip powerUpMusic;

    public override void Consume()
    {
        GameManager.GetPlayer().UpdateFireRate(fireRate);
        GameObject timerObject = new GameObject();
        timer = timerObject.AddComponent<Timer>();
        timer.SetTimer(duration, isDestroyedOnEnd: true);
        timer.TimerEnded += UpgradeDurationEnd;
        timer.StartTimer();
        AudioManager.Instance.SetEffect(POWER_UP_MUSIC_LABEL, powerUpMusic);
        AudioManager.Instance.PlayEffect(POWER_UP_MUSIC_LABEL);
    }

    private void UpgradeDurationEnd()
    {
        GameManager.GetPlayer().ResetFireRate();
        AudioManager.Instance.StopEffect(POWER_UP_MUSIC_LABEL);
    }
}
