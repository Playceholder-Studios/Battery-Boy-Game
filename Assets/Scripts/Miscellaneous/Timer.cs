using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action TimerEnded;
    private bool hasTimerStarted = false;
    private bool destroyOnEnd = false;
    private float targetTime = 0f;
    private float currentTime;

    void Update()
    {
        if (hasTimerStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                hasTimerStarted = false;
                TimerEnded();
                if (destroyOnEnd)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetTimer(float duration, bool isDestroyedOnEnd = false)
    {
        if (duration > 0)
        {
            targetTime = duration;
            currentTime = targetTime;
            destroyOnEnd = isDestroyedOnEnd;
        }
    }

    public void StartTimer()
    {
        hasTimerStarted = true;
        currentTime = targetTime;
    }

}
