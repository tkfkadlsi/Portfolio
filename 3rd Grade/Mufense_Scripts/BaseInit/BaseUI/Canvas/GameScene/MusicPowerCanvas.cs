using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MusicPowerCanvas : BaseCanvas
{
    private enum ESliders
    {
        MusicPowerSlider
    }

    private Slider _musicPowerSlider;

    protected override void Init()
    {
        base.Init();

        Bind<Slider>(typeof(ESliders));

        _musicPowerSlider = Get<Slider>((int)ESliders.MusicPowerSlider);

        SetEnable(true);
    }

    public void SetMaxMusicPower(int maxMusicPower)
    {
        _musicPowerSlider.maxValue = maxMusicPower;
    }

    public void SetMusicPower(int musicPower)
    {
        _musicPowerSlider.DOValue(musicPower, Managers.Instance.Game.UnitTime);
    }
}
