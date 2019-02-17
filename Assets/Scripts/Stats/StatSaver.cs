using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Used the following Unity tutorial as base: https://unity3d.com/learn/tutorials/topics/scripting/introduction-saving-and-loading
public class StatSaver : MonoBehaviour {

    public Stats statData;
    const string folderName = "Stats";
    const string fileName = "stats.dat";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Stats");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    public void Save()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string dataPath = Path.Combine(folderPath, fileName);
        SaveStats(statData, dataPath);
    }

    public void Load()
    {
        string[] filePaths = GetFilePaths();

        if (filePaths.Length > 0)
            statData = LoadStats(filePaths[0]);
    }

    static void SaveStats(Stats data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    static Stats LoadStats(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (Stats)binaryFormatter.Deserialize(fileStream);
        }
    }

    static string[] GetFilePaths()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        return Directory.GetFiles(folderPath, fileName);
    }
}
