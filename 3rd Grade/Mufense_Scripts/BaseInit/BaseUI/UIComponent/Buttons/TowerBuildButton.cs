using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerBuildButton : BaseButton, IPulseable
{
    [SerializeField] private TowerType _type;

    enum ETexts
    {
        CountText
    }

    private TextMeshProUGUI _countText;

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _countText = Get<TextMeshProUGUI>((int)ETexts.CountText);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.BeatEvent += Pulse;
    }

    protected override void Disable()
    {
        base.Disable();

        if(Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= Pulse;
        }
    }

    public void Pulse()
    {
        _button.image.color = new Color(0.5f, 0.5f, 0.5f);
        _button.image.DOColor(Color.white, Managers.Instance.Game.UnitTime * 0.75f);
    }

    public void SetCountText(int count)
    {
        _countText.text = count.ToString();
    }

    protected override void ButtonHandler()
    {
        Managers.Instance.Game.GetComponentInScene<TowerBuilder>().TowerBuild(_type);
        Managers.Instance.UI.GetRootUI().GetCanvas<TowerBuildCanvas>().ClosePanel();
    }
}
