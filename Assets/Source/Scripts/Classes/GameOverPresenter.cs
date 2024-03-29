﻿using Zenject;

public class GameOverPresenter : IInitializable
{
    private GameOverSystem _gameOverSystem;
    private GameOverView _gameOverView;
    private SaveData _save;
    private GameData _game;

    public GameOverPresenter(GameOverSystem gameOverSystem, GameOverView gameOverView, SaveData saveData, GameData game)
    {
        _gameOverSystem = gameOverSystem;
        _gameOverView = gameOverView;
        _save = saveData;
        _game = game;
    }

    public void Initialize()
    {
        _gameOverSystem.OnGameOverEvent += OpenGameOverWindow;
    }

    private void OpenGameOverWindow()
    {
        _gameOverSystem.OnGameOverEvent -= OpenGameOverWindow;
        _gameOverView.gameObject.SetActive(true);
        _gameOverView.RestartButton.onClick.AddListener(_gameOverSystem.RestartGame);

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _gameOverView.ScoreText.text = _game.Score.ToString();
        _gameOverView.RecordScoreText.text = _save.RecordScore.ToString();
    }
}
