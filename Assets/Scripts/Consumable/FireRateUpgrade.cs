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
    public override void Consume()
    {
        GetPlayer().UpdateFireRate(fireRate);
        GameObject timer = new GameObject();
        Timer t = timer.AddComponent<Timer>();
        t.SetTimer(duration);
        t.TimerEnded += UpgradeDurationEnd;
        t.StartTimer();
        
    }

    private void UpgradeDurationEnd()
    {
        Debug.Log("Resetting Fire Rate");
        GetPlayer().ResetFireRate();
    }
}
