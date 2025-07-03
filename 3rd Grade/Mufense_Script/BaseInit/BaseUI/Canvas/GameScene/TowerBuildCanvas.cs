using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildCanvas : BaseCanvas, IOpenClosePanel
{
    enum EImages
    {
        Panel
    }

    private Image _panel;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));

        _panel = Get<Image>((int)EImages.Panel);

        SetEnable(false);
    }

    public void OpenPanel()
    {
        SetEnable(true);
        _panel.rectTransform.anchoredPosition = new Vector2(0, -200f);
        _panel.rectTransform.DOAnchorPosY(0f, Managers.Instance.Game.UnitTime, true);
    }

    public void ClosePanel()
    {
        _panel.rectTransform.DOAnchorPosY(-200f, Managers.Instance.Game.UnitTime, true).OnComplete(() =>
        {
            SetEnable(false);
        });
    }
}
