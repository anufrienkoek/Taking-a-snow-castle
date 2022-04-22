using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.tsc";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        
        binaryFormatter.Serialize(fileStream, progressData);
        fileStream.Close();
    }

    public static ProgressData Load()
    {
        string path = Application.persistentDataPath + "/progress.tsc";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
            fileStream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("No file");
            return null;
        }
    }

    public static void DeleteFile()
    {
        string path = Application.persistentDataPath + "/progress.tsc";
        File.Delete(path);
    }
}
