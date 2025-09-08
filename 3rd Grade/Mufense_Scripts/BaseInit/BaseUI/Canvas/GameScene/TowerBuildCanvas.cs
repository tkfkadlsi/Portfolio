using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildCanvas : BaseCanvas, IOpenClosePanel
{
    private Dictionary<TowerType, TowerBuildButton> _towerButtonDictionary = new Dictionary<TowerType, TowerBuildButton>();

    enum EImages
    {
        Panel
    }

    enum EButtons
    {
        BackGround
    }

    enum ETowerBuildButtons
    {
        PianoButton,
        DrumButton,
        GuitarButton,
        ViolinButton
    }

    private Image _panel;

    private Button _button;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));
        Bind<Button>(typeof(EButtons));
        Bind<TowerBuildButton>(typeof(ETowerBuildButtons));

        _towerButtonDictionary.Add(TowerType.Piano, Get<TowerBuildButton>((int)ETowerBuildButtons.PianoButton));
        _towerButtonDictionary.Add(TowerType.Drum, Get<TowerBuildButton>((int)ETowerBuildButtons.DrumButton));
        _towerButtonDictionary.Add(TowerType.Guitar, Get<TowerBuildButton>((int)ETowerBuildButtons.GuitarButton));
        _towerButtonDictionary.Add(TowerType.Violin, Get<TowerBuildButton>((int)ETowerBuildButtons.ViolinButton));

        _panel = Get<Image>((int)EImages.Panel);
        _button = Get<Button>((int)EButtons.BackGround);

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
        _panel.rectTransform.anchoredPosition = new Vector2(0, -200f);
        _panel.rectTransform.DOAnchorPosY(0f, 0.25f, true);

        foreach (var kv in _towerButtonDictionary)
        {
            kv.Value.gameObject.SetActive(Managers.Instance.Data.TowerCountManagement.CanBuildTower(kv.Key));
            kv.Value.SetCountText(Managers.Instance.Data.TowerCountManagement.CanBuildTowerCount(kv.Key));
        }
    }

    public void ClosePanel()
    {
        _panel.rectTransform.DOAnchorPosY(-200f, 0.25f, true).OnComplete(() =>
        {
            SetEnable(false);
        });
    }

    private void ButtonHandler()
    {
        ClosePanel();
    }
}
