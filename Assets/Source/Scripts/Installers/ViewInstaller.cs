using UnityEngine;
using Zenject;
public class ViewInstaller : MonoInstaller
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private GameOverView _gameOverView;

    public override void InstallBindings()
    {
        Container.Bind<IInitializable>().To<ScorePresenter>().AsSingle();
        Container.Bind<IInitializable>().To<GameOverPresenter>().AsSingle();

        Container.Bind<ScoreView>().FromInstance(_scoreView).AsSingle();
        Container.Bind<GameOverView>().FromInstance(_gameOverView).AsSingle();
    }
}
