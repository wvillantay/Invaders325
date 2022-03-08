using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveProfile(Menu menu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.lafe";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(menu);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadProfile()
    {
        string path = Application.persistentDataPath + "/save.lafe";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No Save File Located at " + path);
            return null;
        }
    }

    public static void DeleteProfile()
    {
        string path = Application.persistentDataPath + "/save.lafe";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}