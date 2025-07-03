using System;
using UnityEngine;

public class MusicPowerData : MonoBehaviour
{
    private int _maxMusicPower;
    private int _musicPower;

    private void Start()
    {
        SetMaxMusicPower(1000);
        AddMusicPower(500);
    }

    public void AddMusicPower(int value)
    {
        _musicPower += value;
        if( _musicPower > _maxMusicPower )
        {
            _musicPower = _maxMusicPower;
        }

        Managers.Instance.UI.GetRootUI().GetCanvas<MusicPowerCanvas>().SetMusicPower(_musicPower);
    }

    public bool RemoveMusicPower(int value)
    {
        if (_musicPower < value)
        {
            return false;
        }
        else
        {
            _musicPower -= value;
            Managers.Instance.UI.GetRootUI().GetCanvas<MusicPowerCanvas>().SetMusicPower(_musicPower);

            return true;
        }
    }

    public void SetMaxMusicPower(int value)
    {
        _maxMusicPower = value;

        Managers.Instance.UI.GetRootUI().GetCanvas<MusicPowerCanvas>().SetMaxMusicPower(_maxMusicPower);
    }
}
