using YG;

public class LevelLoaderSystem : GameSystem
{
    public override void OnAwake()
    {
        YandexGame.Instance._FullscreenShow();
        _game.Level = FindObjectOfType<Level>();
    }
}
