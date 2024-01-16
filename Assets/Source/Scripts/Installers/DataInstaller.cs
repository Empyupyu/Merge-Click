using Zenject;
public class DataInstaller : MonoInstaller
{
    private SaveData _saveData;

    public override void InstallBindings()
    {
        LoadSaveDatas();
        Container.Bind<SaveData>().FromInstance(_saveData).AsSingle();
        Container.Bind<GameData>().FromNew().AsSingle();
    }

    private void LoadSaveDatas()
    {
        _saveData = new();

        if (!SaveUtility.Instance().HasSave()) return;

        _saveData = SaveUtility.Instance().Load();
    }
}
