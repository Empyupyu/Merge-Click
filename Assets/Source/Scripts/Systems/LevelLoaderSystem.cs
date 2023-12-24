public class LevelLoaderSystem : GameSystem
{
    public override void OnAwake()
    {
        _game.Level = FindObjectOfType<Level>();
    }
}
