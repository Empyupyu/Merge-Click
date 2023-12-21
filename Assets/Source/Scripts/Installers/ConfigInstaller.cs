using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller
{
    [SerializeField] private AnimalConfigData _animalConfigData;

    public override void InstallBindings()
    {
        Container.Bind<AnimalConfigData>().FromScriptableObject(_animalConfigData).AsSingle();
    }
}
