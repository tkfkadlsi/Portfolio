using UnityEngine;

public class MusicPowerData : MonoBehaviour
{
    private int _maxMusicPower;
    private int _musicPower;

    private void Start()
    {
        _maxMusicPower = 100;
        _musicPower = 0;
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
    }
}
