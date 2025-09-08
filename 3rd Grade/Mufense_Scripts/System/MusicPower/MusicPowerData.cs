using TMPro.EditorUtilities;
using UnityEngine;

public class MusicPowerData : MonoBehaviour
{
    private int _maxMusicPower;
    private int _musicPower;

    private int _coreMusicPower;
    public int CoreMusicPower
    {
        get
        {
            return _coreMusicPower;
        }
        set
        {
            _coreMusicPower = value;
        }
    }

    private void Start()
    {
        _maxMusicPower = 100;
        _musicPower = 0;
        _coreMusicPower = 0;
        SyncUI();
    }

    public void AddMusicPower(int value)
    {
        _musicPower += value;
        if (_musicPower >= _maxMusicPower)
        {
            _musicPower -= _maxMusicPower;
            _maxMusicPower += 100;

            MasteryLevelUp();
        }

        SyncUI();
    }

    private void MasteryLevelUp()
    {
        Managers.Instance.UI.GetRootUI().GetCanvas<CardCanvas>().OpenPanel();
    }

    private void SyncUI()
    {
        Managers.Instance.UI.GetRootUI().GetCanvas<MusicPowerCanvas>().SetMaxMusicPower(_maxMusicPower);
        Managers.Instance.UI.GetRootUI().GetCanvas<MusicPowerCanvas>().SetMusicPower(_musicPower);
        Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SyncUI();
    }
}
