using UnityEngine;

public class GameOverCheckerSystem : GameSystem
{
    public override void OnAwake()
    {
        _game.Level.GameOverListener.OnTriggerEnterEvent += StartLoseTimer;
        _game.Level.GameOverListener.OnTriggerExitEvent += StopLoseTimer;
    }

    private void StartLoseTimer(Transform target)
    {
        if (!IsAnimal(target)) return;
    }

    private void StopLoseTimer(Transform target)
    {
        if (!IsAnimal(target)) return;
    }

    private bool IsAnimal(Transform target)
    {
        return target.TryGetComponent<Animal>(out var animal);
    }
}
