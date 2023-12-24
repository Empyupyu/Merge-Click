using UnityEngine;
using Zenject;
public class ViewInstaller : MonoInstaller
{
    [SerializeField] private ScoreView _scoreView;

    public override void InstallBindings()
    {
        Container.Bind<IInitializable>().To<ScoreController>().AsSingle();
        Container.Bind<ScoreView>().FromInstance(_scoreView).AsSingle();
    }
}
