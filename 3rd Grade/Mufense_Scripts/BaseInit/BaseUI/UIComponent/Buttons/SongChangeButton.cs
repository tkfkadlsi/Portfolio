using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SongChangeButton : BaseButton, IPulseable, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MusicType _type;

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.BeatEvent += Pulse;
    }

    protected override void Disable()
    {
        base.Disable();

        if (Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= Pulse;
        }
    }

    public void Pulse()
    {
        _button.image.color = new Color(0.25f, 0.25f, 0.25f);
        _button.image.DOColor(Color.white, Managers.Instance.Game.UnitTime * 0.75f);
    }

    protected override void ButtonHandler()
    {
        Managers.Instance.Game.GetComponentInScene<MusicPlayer>().ChangeMusic(_type);
        Managers.Instance.UI.GetRootUI().GetCanvas<SongChangeCanvas>().ClosePanel();
        Managers.Instance.UI.GetRootUI().GetCanvas<SongInfoCanvas>().ClosePanel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.Instance.UI.GetRootUI().GetCanvas<SongInfoCanvas>().SetMusic(Managers.Instance.Game.GetComponentInScene<MusicDataStorage>().MusicDictionary[_type]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.Instance.UI.GetRootUI().GetCanvas<SongInfoCanvas>().ClosePanel();
    }
}
