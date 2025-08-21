using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using TMPro.EditorUtilities;

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
        CookieRequestButton
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
        if(GetEnable() == true)
        {
            Managers.Instance.Game.GetComponentInScene<MusicPlayer>().GetPlayableMusicList(_playableMusicList);

            foreach(SongChangeButton button in _changeButtonDictionary.Values)
            {
                button.gameObject.SetActive(false);
            }

            foreach(Music music in _playableMusicList)
            {
                if(music != Managers.Instance.Game.PlayingMusic && music.Clip.length >= Managers.Instance.Game.GetComponentInScene<MusicPlayer>().MusicPlayTime)
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
        _panel.rectTransform.DOAnchorPosY(0f, Managers.Instance.Game.UnitTime, true);
    }

    public void ClosePanel()
    {
        _panel.rectTransform.DOAnchorPosY(-200f, Managers.Instance.Game.UnitTime, true).OnComplete(() =>
        {
            SetEnable(false);
        });
    }

    private void ButtonHandler()
    {
        ClosePanel();
    }
}
