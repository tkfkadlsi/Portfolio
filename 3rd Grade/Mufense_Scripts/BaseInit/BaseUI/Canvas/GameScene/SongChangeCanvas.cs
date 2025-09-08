using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongChangeCanvas : BaseCanvas, IOpenClosePanel
{
    private List<Music> _playableMusicList = new List<Music>();
    private Dictionary<MusicType, SongChangeButton> _changeButtonDictionary = new Dictionary<MusicType, SongChangeButton>();

    enum EImages
    {
        Panel
    }

    enum ESongChangeButtons
    {
        VictoryButton,
        SecretLibraryButton,
        In59SecButton,
        CookieRequestButton,
        WaterColorButton,
        ZeroOneButton
    }

    enum EButtons
    {
        BackGround
    }

    private Image _panel;

    private Button _button;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));
        Bind<SongChangeButton>(typeof(ESongChangeButtons));
        Bind<Button>(typeof(EButtons));

        _panel = Get<Image>((int)EImages.Panel);

        //_changeButtonDictionary.Add(MusicType.Katamari, Get<SongChangeButton>((int)ESongChangeButtons.KatamariButton));
        _changeButtonDictionary.Add(MusicType.Victory, Get<SongChangeButton>((int)ESongChangeButtons.VictoryButton));
        _changeButtonDictionary.Add(MusicType.SecretLibrary, Get<SongChangeButton>((int)ESongChangeButtons.SecretLibraryButton));
        _changeButtonDictionary.Add(MusicType.In59Sec, Get<SongChangeButton>((int)ESongChangeButtons.In59SecButton));
        _changeButtonDictionary.Add(MusicType.CookieRequest, Get<SongChangeButton>((int)ESongChangeButtons.CookieRequestButton));
        _changeButtonDictionary.Add(MusicType.InTheSea, Get<SongChangeButton>((int)ESongChangeButtons.WaterColorButton));
        _changeButtonDictionary.Add(MusicType.Zeroone, Get<SongChangeButton>((int)ESongChangeButtons.ZeroOneButton));

        _button = Get<Button>((int)EButtons.BackGround);

        _button.onClick.AddListener(ButtonHandler);

        SetEnable(false);
    }

    protected override void Release()
    {
        base.Release();

        _button.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (GetEnable() == true)
        {
            Managers.Instance.Game.GetComponentInScene<MusicPlayer>().GetPlayableMusicList(_playableMusicList);

            foreach (SongChangeButton button in _changeButtonDictionary.Values)
            {
                button.gameObject.SetActive(false);
            }

            foreach (Music music in _playableMusicList)
            {
                if (music != Managers.Instance.Game.PlayingMusic && music.Clip.length >= Managers.Instance.Game.GetComponentInScene<MusicPlayer>().MusicPlayTime)
                {
                    _changeButtonDictionary[music.Type].gameObject.SetActive(true);
                }
            }
        }
    }

    public void OpenPanel()
    {
        SetEnable(true);
        _panel.rectTransform.anchoredPosition = new Vector2(0, -200f);
        _panel.rectTransform.DOAnchorPosY(0f, 0.25f, true);
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
