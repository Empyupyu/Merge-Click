using UnityEngine;

public class SaveUtility
{
    private const string SAVE_DATA_KEY = "SaveDataKey";

    private static SaveUtility _instance;

    public static SaveUtility Instance()
    {
        if (_instance == null)
        {
            _instance = new SaveUtility();
        }

        return _instance;
    }

    public void Save(SaveData saveData)
    {
        var save = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_DATA_KEY, save);
        PlayerPrefs.Save();
    }

    public SaveData Load()
    {
        var save = PlayerPrefs.GetString(SAVE_DATA_KEY);
        SaveData _saveData = JsonUtility.FromJson<SaveData>(save);
        return _saveData;
    }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey(SAVE_DATA_KEY);
    }
}
