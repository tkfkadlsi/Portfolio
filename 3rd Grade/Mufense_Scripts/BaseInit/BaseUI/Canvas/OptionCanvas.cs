using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionCanvas : BaseCanvas, IOpenClosePanel
{
    [SerializeField] private AudioMixer _audioMixer;

    enum ESliders
    {
        BGMVolumeSlider,
        SFXVolumeSlider
    }

    enum EButtons
    {
        BackGround
    }

    enum EImages
    {
        OptionPanel
    }

    private Slider _bgmSlider;
    private Slider _sfxSlider;

    private Button _backGroundButton;

    private Image _optionPanel;

    protected override void Init()
    {
        base.Init();

        Bind<Slider>(typeof(ESliders));
        Bind<Button>(typeof(EButtons));
        Bind<Image>(typeof(EImages));

        _bgmSlider = Get<Slider>((int)ESliders.BGMVolumeSlider);
        _sfxSlider = Get<Slider>((int)ESliders.SFXVolumeSlider);

        _backGroundButton = Get<Button>((int)EButtons.BackGround);

        _optionPanel = Get<Image>((int)EImages.OptionPanel);

        _bgmSlider.onValueChanged.AddListener(BgmSliderHandler);
        _sfxSlider.onValueChanged.AddListener(SfxSliderHandler);
        _backGroundButton.onClick.AddListener(BackGroundButtonHandler);

        float bgm = Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().GetData(_bgmKey);
        bgm += 80f;

        float sfx = Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().GetData(_sfxKey);
        sfx += 80f;

        _bgmSlider.value = bgm;
        _sfxSlider.value = sfx;

        _optionPanel.rectTransform.localScale = Vector3.zero;
        SetEnable(false);
    }

    protected override void Release()
    {
        base.Release();


        _bgmSlider.onValueChanged.RemoveAllListeners();
        _sfxSlider.onValueChanged.RemoveAllListeners();
        _backGroundButton.onClick.RemoveAllListeners();
    }

    private const string _bgmKey = "BGM";
    private const string _sfxKey = "SFX";

    private void BgmSliderHandler(float value)
    {
        value -= 80;

        Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().SetData(_bgmKey, value);
        _audioMixer.SetFloat(_bgmKey, value);
    }

    private void SfxSliderHandler(float value)
    {
        value -= 80;

        Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().SetData(_sfxKey, value);
        _audioMixer.SetFloat(_sfxKey, value);
    }

    private void BackGroundButtonHandler()
    {
        ClosePanel();
    }

    public void OpenPanel()
    {
        DOTween.Kill(_optionPanel);
        _optionPanel.rectTransform.DOScale(1, 0.25f);

        _bgmSlider.value = Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().GetData(_bgmKey) + 80f;
        _sfxSlider.value = Managers.Instance.Game.GetComponentInScene<SaveAndLoad>().GetData(_sfxKey) + 80f;
    }

    public void ClosePanel()
    {
        DOTween.Kill(_optionPanel);
        _optionPanel.rectTransform.DOScale(0, 0.25f).OnComplete(() =>
        {
            SetEnable(false);
        });
    }
}
