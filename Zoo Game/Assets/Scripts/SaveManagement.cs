using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManagement
{
    public static void SaveWorld(WorldManagement worldScript)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/world.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData(worldScript);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static WorldData LoadWorld()
    {
        string path = Application.persistentDataPath + "/world.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File not found");
            return null;
        }
    }
}
