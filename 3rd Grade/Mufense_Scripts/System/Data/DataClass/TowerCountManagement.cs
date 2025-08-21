using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerCountManagement
{
    private Dictionary<TowerType, int> TowerCountDictionary = new Dictionary<TowerType, int>();

    public void Init()
    {
        TowerCountDictionary.Clear();

        foreach(TowerType type in Enum.GetValues(typeof(TowerType)))
        {
            TowerCountDictionary.Add(type, 0);
        }
    }

    private int GetMaxTowerCount(TowerType type)
    {
        return 1 + Managers.Instance.Game.GetCardCount(Managers.Instance.Data.ConvertData.TowerType2CountCardType[type]);
    }

    public bool CanBuildTower(TowerType type)
    {
        return TowerCountDictionary[type] < GetMaxTowerCount(type);
    }

    public int CanBuildTowerCount(TowerType type)
    {
        return GetMaxTowerCount(type) - TowerCountDictionary[type];
    }

    public void BuildTower(TowerType type)
    {
        if(CanBuildTower(type) == false)
        {
            Debug.LogError("타워 개수 초과했는데 이거 실행하지 마세요");
            return;
        }

        TowerCountDictionary[type]++;
    }

    public void BreakTower(TowerType type)
    {
        if (TowerCountDictionary[type] == 0)
        {
            Debug.LogError("타워가 없는데 이거 실행하지 마세요");
            return;
        }

        TowerCountDictionary[type]--;
    }
}
