using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public class DataClass
{
    public SerializedDictionary<string, float> FloatDataDictionary = new SerializedDictionary<string, float>();

    public void ResetData()
    {
        FloatDataDictionary.Clear();
    }
}
