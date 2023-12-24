using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private MergeSystem _mergeSystem;
    public override void InstallBindings()
    {
        Container.Bind<MergeSystem>().FromInstance(_mergeSystem).AsSingle();
    }
}
