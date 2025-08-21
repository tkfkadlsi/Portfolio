using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class TowerDataManagement
{
    [SerializedDictionary("Type", "DataSO")]
    public SerializedDictionary<TowerType, TowerDataSO> _towerDatas = new SerializedDictionary<TowerType, TowerDataSO>();

    public float GetDamage(TowerType type)
    {
        CardType card = Managers.Instance.Data.ConvertData.TowerType2DamageCardType[type];

        int level = 1 + Managers.Instance.Game.GetCardCount(card);

        return _towerDatas[type].Damage[level];
    }

    public float GetRange(TowerType type, int level = 0)
    {
        if(type == TowerType.Drum)
        {
            return _towerDatas[type].Range[level];
        }

        CardType card = Managers.Instance.Data.ConvertData.TowerType2RangeCardType[type];

        level = 1 + Managers.Instance.Game.GetCardCount(card);

        return _towerDatas[type].Range[level];
    }

    public string GetDescription(TowerType type)
    {
        return _towerDatas[type].Description;
    }
}
