using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller
{
    [SerializeField] private AnimalConfigData _animalConfigData;
    [SerializeField] private MovingConfigData _movingConfigData;
    [SerializeField] private AnimalEvolutionConfigData _animalEvolutionConfigData;

    public override void InstallBindings()
    {
        Container.Bind<AnimalConfigData>().FromScriptableObject(_animalConfigData).AsSingle();
        Container.Bind<MovingConfigData>().FromScriptableObject(_movingConfigData).AsSingle();
        Container.Bind<AnimalEvolutionConfigData>().FromScriptableObject(_animalEvolutionConfigData).AsSingle();
    }
}
