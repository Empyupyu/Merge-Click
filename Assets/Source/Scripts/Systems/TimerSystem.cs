public class TimerSystem : GameSystem
{
    public override void OnAwake()
    {
        _game.ActivateTimerSingal.AddListener(EnableTimer);
    }

    private void EnableTimer(Timer timer, bool isActive)
    {
        if(isActive)
        {
            _game.Timers.Add(timer);
            return;
        }

        _game.Timers.Remove(timer);
    }

    public override void OnUpdate()
    {
        Tick();
    }

    private void Tick()
    {
        for (int i = 0; i < _game.Timers.Count; i++)
        {
            var timer = _game.Timers[i];
            timer.Tick();

            if (!TryRemoveTimer(timer)) continue;

            i--;
        }
    }

    private bool TryRemoveTimer(Timer timer)
    {
        if (timer.Value > 0) return false;

        _game.Timers.Remove(timer);

        return true;
    }
}
