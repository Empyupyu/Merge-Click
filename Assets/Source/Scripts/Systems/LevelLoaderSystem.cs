using UnityEngine;
using YG;

public class LevelLoaderSystem : GameSystem
{
    public override void OnAwake()
    {
        YandexGame.Instance._FullscreenShow();
        LevelLoad();
    }

    private void LevelLoad()
    {
        var levelPrefab = Resources.Load<Level>($"Levels/Level {(_save.LevelIndex + 1)}");
        _game.Level = Instantiate(levelPrefab);
    }
}
