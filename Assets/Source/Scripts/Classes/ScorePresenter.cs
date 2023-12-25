﻿using Zenject;
public class ScorePresenter : IInitializable
{
    private MergeSystem _mergeSystem;
    private ScoreView _scoreView;
    private SaveData _save;
    private GameData _game;

    public ScorePresenter(MergeSystem mergeSystem, ScoreView scoreView, SaveData saveData, GameData game)
    {
        _mergeSystem = mergeSystem;
        _scoreView = scoreView;
        _save = saveData;
        _game = game;
    }

    public void Initialize()
    {
        _mergeSystem.OnMergeEvent += UpdateScoreText;

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreView.ScoreText.text = _game.Score.ToString();
        _scoreView.RecordScoreText.text = _save.RecordScore.ToString();
    }
}
