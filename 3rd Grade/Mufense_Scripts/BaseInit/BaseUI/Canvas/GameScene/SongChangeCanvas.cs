using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

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
        KatamariButton,
        VictoryButton
    }

    private Image _panel;

    protected override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EImages));
        Bind<SongChangeButton>(typeof(ESongChangeButtons));

        _panel = Get<Image>((int)EImages.Panel);

        _changeButtonDictionary.Add(MusicType.Katamari, Get<SongChangeButton>((int)ESongChangeButtons.KatamariButton));
        _changeButtonDictionary.Add(MusicType.Victory, Get<SongChangeButton>((int)ESongChangeButtons.VictoryButton));

        SetEnable(false);
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
}
