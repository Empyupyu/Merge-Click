using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSystem : GameSystem
{
    public event Action OnGameOverEvent;
    private Timer _gameOverTimer;

    public override void OnAwake()
    {
        _game.Level.GameOverListener.OnTriggerEnterEvent += StartLoseTimer;
        _game.Level.GameOverListener.OnTriggerExitEvent += StopLoseTimer;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartLoseTimer(Transform target)
    {
        if (!IsAnimal(target)) return;

        _gameOverTimer = new Timer(1.5f);
        _game.ActivateTimerSingal.Dispatch(_gameOverTimer, true);
        _game.TimerCompletedSingal.AddListener(GameOver);
    }

    private void GameOver()
    {
        OnGameOverEvent?.Invoke();
        RemoveGameOverListener();
    }

    private void StopLoseTimer(Transform target)
    {
        if (!IsAnimal(target)) return;

        RemoveGameOverListener();
    }

    private bool IsAnimal(Transform target)
    {
        return target.TryGetComponent<Animal>(out var animal);
    }

    private void RemoveGameOverListener()
    {
        _game.ActivateTimerSingal.Dispatch(_gameOverTimer, false);
        _game.TimerCompletedSingal.RemoveListener(GameOver);
    }
}
