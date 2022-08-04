using UnityEngine;

public class FireRateUpgrade : PlayerConsumable
{
    /// <summary>
    /// 
    /// </summary>
    public float fireRate = 0.5f;

    /// <summary>
    /// The length that the upgrade lasts
    /// </summary>
    public float duration = 2f;

    private Timer timer;
    public override void Consume()
    {
        GetPlayer().UpdateFireRate(fireRate);
        GameObject timerObject = new GameObject();
        timer = timerObject.AddComponent<Timer>();
        timer.SetTimer(duration, isDestroyedOnEnd: true);
        timer.TimerEnded += UpgradeDurationEnd;
        timer.StartTimer();
    }

    private void UpgradeDurationEnd()
    {
        GetPlayer().ResetFireRate();
    }
}
