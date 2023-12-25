using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller
{
    [SerializeField] private AnimalConfigData _animalConfigData;
    [SerializeField] private MovingConfigData _movingConfigData;
    [SerializeField] private MergeConfigData _mergeConfigData;

    public override void InstallBindings()
    {
        Container.Bind<AnimalConfigData>().FromScriptableObject(_animalConfigData).AsSingle();
        Container.Bind<MovingConfigData>().FromScriptableObject(_movingConfigData).AsSingle();
        Container.Bind<MergeConfigData>().FromScriptableObject(_mergeConfigData).AsSingle();
    }
}
