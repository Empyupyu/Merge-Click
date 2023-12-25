using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private MergeSystem _mergeSystem;
    [SerializeField] private GameOverSystem _gameOverSystem;

    public override void InstallBindings()
    {
        Container.Bind<MergeSystem>().FromInstance(_mergeSystem).AsSingle();
        Container.Bind<GameOverSystem>().FromInstance(_gameOverSystem).AsSingle();
    }
}
