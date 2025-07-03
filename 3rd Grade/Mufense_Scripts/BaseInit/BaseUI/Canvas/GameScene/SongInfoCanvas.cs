using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SongInfoCanvas : BaseCanvas, IOpenClosePanel
{
    enum EImages
    {
        Panel
    }

    enum ESongInfoPanels
    {
        Panel
    }

    private Image _panel;

    private SongInfoPanel _songInfoPanel;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));
        Bind<SongInfoPanel>(typeof(ESongInfoPanels));

        _panel = Get<Image>((int)EImages.Panel);
        _songInfoPanel = Get<SongInfoPanel>((int)ESongInfoPanels.Panel);

        SetEnable(false);
    }

    public void OpenPanel()
    {
        DOTween.Kill(_panel.rectTransform);
        SetEnable(true);
        _panel.rectTransform.localScale = Vector3.zero;
        _panel.rectTransform.DOScale(1f, Managers.Instance.Game.UnitTime);
    }

    public void ClosePanel()
    {
        DOTween.Kill(_panel.rectTransform);
        _panel.rectTransform.DOScale(0f, Managers.Instance.Game.UnitTime).OnComplete(() =>
        {
            SetEnable(false);
            Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SetEnable(true);
        });
    }

    public void SetMusic(Music music)
    {
        OpenPanel();
        _songInfoPanel.SetMusic(music);
    }
}
