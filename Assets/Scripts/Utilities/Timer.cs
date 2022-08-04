using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool IsRunning { get; private set; }
    public float CurrentTime { get; private set; }

    public Action TimerStarted;

    public Action TimerEnded;

    private float m_countdownTime;

    private void Update()
    {
        if (IsRunning)
        {
            CurrentTime -= Time.deltaTime;
            IsRunning = CurrentTime > 0;
            if (CurrentTime <= 0f)
            {
                OnTimerEnd();
            }
        }
    }

    private void OnTimerStart()
    {
        TimerStarted?.Invoke();
        Debug.Log("Timer Starting");
    }

    private void OnTimerEnd()
    {
        TimerEnded?.Invoke();
        Debug.Log("Timer Ending");
    }

    public void SetTimer(float countdownTime)
    {
        m_countdownTime = countdownTime;
        CurrentTime = m_countdownTime;
    }

    public void ResetTimer()
    {
        CurrentTime = m_countdownTime;
        IsRunning = false;
    }

    public void StartTimer()
    {
        if (CurrentTime > 0)
        {
            IsRunning = true;
            OnTimerStart();
        }
    }
}
