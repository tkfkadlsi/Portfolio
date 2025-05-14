using DG.Tweening;
using System;
using System.IO;
using System.Reflection;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{

    private string dataPath = "PlayData.json";

    private Information information;

    private void Awake()
    {
        information = GetComponent<Information>();
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        if (File.Exists(path))
        {
            LoadData();
        }
        else
        {
            ResetData();
        }
    }

    public void ResetData()
    {
        Debug.Log("�����͸� �ʱ�ȭ��");
        information.GameData = new GameData();
        Array.Fill(information.GameData.BestStarRatingDream, 0);
        Array.Fill(information.GameData.BestStarRatingNightmare, 0);
        Array.Fill(information.GameData.BestDreamAccuraries, 0);
        Array.Fill(information.GameData.BestNightmareAccuraries, 0);
        Array.Fill(information.GameData.IsDreamClear, false);
        Array.Fill(information.GameData.IsNightClear, false);

        information.GameData.Nickname = "";
        information.GameData.EnterTicket = 10;
        information.GameData.Exp = 0;
        information.GameData.LV = 1;
        information.GameData.Coin = 0;
        information.GameData.IsAchieveUnLock.Add(AchieveType.Hero_In_The_Fairytale);
        information.GameData.IsSkinUnLock.Add(SkinType.Default);
        information.GameData.selectedAchieve = AchieveType.Hero_In_The_Fairytale;
        information.GameData.selectedSkin = SkinType.Default;
        SaveData();
    }

    public void SaveData()
    {
        Debug.Log("�����͸� ������");
        string path = Path.Combine(Application.persistentDataPath, dataPath);

        string jsonData = JsonUtility.ToJson(information.GameData, true);
        File.WriteAllText(path, jsonData);
        Debug.Log("���������� �����.");
    }

    public void LoadData()
    {
        Debug.Log("�����͸� �ҷ���");
        information.GameData = new GameData();
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        string loadJsonData = File.ReadAllText(path);
        if (loadJsonData == "")
        {
            Debug.Log("�������� �𸣰����� �����Ͱ� �ʱ�ȭ ��. Ȥ�� ����?");
            ResetData();
        }
        information.GameData = JsonUtility.FromJson<GameData>(loadJsonData);
        if (information.GameData.Exp >= 1000)
        {
            information.GameData.Exp = 0;
            Application.Quit();
        }

        Debug.Log(Information.Instance.GameData.PassWord);
        Debug.Log(Information.Instance.GameData.Exp);
        Debug.Log(Information.Instance.GameData.selectedTheme);
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
        DOTween.Clear();
    }
}