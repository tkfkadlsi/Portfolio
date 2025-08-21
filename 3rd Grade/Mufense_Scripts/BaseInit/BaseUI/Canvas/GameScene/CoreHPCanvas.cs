using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CoreHPCanvas : BaseCanvas
{
    private readonly float _time = 0.5f;

    enum ESliders
    {
        CoreHPSlider
    }

    private Slider _coreHPSlider;

    protected override void Init()
    {
        base.Init();

        Bind<Slider>(typeof(ESliders));

        _coreHPSlider = Get<Slider>((int)ESliders.CoreHPSlider);

        Managers.Instance.Game.CoreHPChangeEvent += CoreHpChangeHandler;
    }

    protected override void Release()
    {
        base.Release();

        Managers.Instance.Game.CoreHPChangeEvent -= CoreHpChangeHandler;
    }

    private void CoreHpChangeHandler(float hp)
    {
        DOTween.Kill(this);

        _coreHPSlider.DOValue(hp, _time);
    }
}
