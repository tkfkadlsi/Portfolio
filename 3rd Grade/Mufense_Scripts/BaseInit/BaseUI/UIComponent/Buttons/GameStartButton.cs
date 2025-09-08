using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : BaseButton, IPulseable
{
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
        Managers.Instance.Game.AllClear();
        SceneManager.LoadSceneAsync("GameScene");
    }
}
