using Supyrb;
using UnityEngine;

public class Timer
{
    public float Value { get; private set; }

    public Timer(float value)
    {
        Value = value;
    }

    public void Tick()
    {
        Value -= Time.deltaTime;

        if (Value > 0) return;

        Signals.Get<TimerCompletedSingal>().Dispatch();
    }
}
