using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller
{
    [SerializeField] private AnimalConfigData _animalConfigData;
    [SerializeField] private MovingConfigData _movingConfigData;
    
    public override void InstallBindings()
    {
        Container.Bind<AnimalConfigData>().FromScriptableObject(_animalConfigData).AsSingle();
        Container.Bind<MovingConfigData>().FromScriptableObject(_movingConfigData).AsSingle();
    }
}
