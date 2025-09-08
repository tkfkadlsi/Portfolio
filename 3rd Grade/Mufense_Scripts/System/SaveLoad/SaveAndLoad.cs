using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private const string fileName = "Savedata.json";

    private DataClass data = new DataClass();

    public void SetData(string key, float value)
    {
        if (data.FloatDataDictionary.ContainsKey(key))
        {
            data.FloatDataDictionary[key] = value;
        }
        else
        {
            data.FloatDataDictionary.Add(key, value);
        }
    }

    public float GetData(string key)
    {
        if(data.FloatDataDictionary.ContainsKey(key))
        {
            return data.FloatDataDictionary[key];
        }
        else
        {
            Debug.LogError("데이터 없음");
            return 0f;
        }
    }

    private void Awake()
    {
        data.ResetData();

        string path = Path.Combine(Application.persistentDataPath, fileName);

        DataClass newData = new DataClass();

        if(File.Exists(path) == false)
        {
            newData.FloatDataDictionary.Add("BGM", 0f);
            newData.FloatDataDictionary.Add("SFX", 0f);
        }
        else
        {
            string json = File.ReadAllText(path);
            newData = JsonUtility.FromJson<DataClass>(json);
        }

        foreach(var kv in newData.FloatDataDictionary)
        {
            data.FloatDataDictionary.Add(kv.Key, kv.Value);
        }
    }

    private void OnDestroy()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        DataClass newData = new DataClass();

        foreach(var kv in data.FloatDataDictionary)
        {
            newData.FloatDataDictionary.Add(kv.Key, kv.Value);
        }


        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
}
