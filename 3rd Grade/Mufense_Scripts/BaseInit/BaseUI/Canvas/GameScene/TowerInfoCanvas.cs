using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoCanvas : BaseCanvas, IOpenClosePanel
{
    enum EImages
    {
        Panel
    }

    enum EButtons
    {
        BackGround
    }

    enum ETowerInfoPanel
    {
        Panel
    }

    private Image _panel;

    private Button _button;

    private TowerInfoPanel _infoPanel;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));
        Bind<Button>(typeof(EButtons));
        Bind<TowerInfoPanel>(typeof(ETowerInfoPanel));
        
        _panel = Get<Image>((int)EImages.Panel);
        _button = Get<Button>((int)EButtons.BackGround);
        _infoPanel = Get<TowerInfoPanel>((int)ETowerInfoPanel.Panel);

        _button.onClick.AddListener(ButtonHandler);


        SetEnable(false);
    }

    protected override void Release()
    {
        base.Release();

        _button.onClick.RemoveAllListeners();
    }

    public void OpenPanel()
    {
        SetEnable(true);
        _panel.rectTransform.localScale = Vector3.zero;
        _panel.rectTransform.DOScale(1f, Managers.Instance.Game.UnitTime);
    }

    public void ClosePanel()
    {
        _panel.rectTransform.DOScale(0f, Managers.Instance.Game.UnitTime).OnComplete(() =>
        {
            SetEnable(false);
            Managers.Instance.UI.GetRootUI().GetCanvas<MenuCanvas>().SetEnable(true);
        });
    }

    public void SetTower(Tower tower)
    {
        OpenPanel();
        _infoPanel.SetTower(tower);
    }

    public void SyncUI(Tower caller)
    {
        _infoPanel.SyncUI(caller);
    }

    private void ButtonHandler()
    {
        ClosePanel();
    }
}