using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OnCoreAttackButton : BaseButton, IPulseable
{
    private MusicPowerData _musicPowerData;
    private CoreAttackTower _coreAttackTower;

    private bool _canCoreAttack;
    private bool _isCoreAttack;
    private readonly string _musicPowerWarning = "코어의 뮤직파워가 부족합니다!";
    private readonly string _alreadyCoreAttacking = "이미 사용 중입니다!";

    enum EImages
    {
        DarkScreen
    }

    private Image _darkScreen;

    protected override void Init()
    {
        base.Init();

        _musicPowerData = Managers.Instance.Game.GetComponentInScene<MusicPowerData>();
        _coreAttackTower = Managers.Instance.Game.GetComponentInScene<CoreAttackTower>();

        _canCoreAttack = false;
        _isCoreAttack = false;

        Bind<Image>(typeof(EImages));

        _darkScreen = Get<Image>((int)EImages.DarkScreen);
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.Game.BeatEvent += Pulse;
    }

    protected override void Disable()
    {
        base.Disable();

        if(Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= Pulse;
        }
    }

    public void Pulse()
    {
        if(_canCoreAttack)
        _button.image.color = new Color(0.25f, 0.25f, 0.25f);
        _button.image.DOColor(Color.white, Managers.Instance.Game.UnitTime * 0.75f);
    }

    protected override async void ButtonHandler()
    {
        if (_canCoreAttack == true && _isCoreAttack == false)
        {
            _isCoreAttack = true;
            _coreAttackTower.SetCanAttack(true);
            _musicPowerData.CoreMusicPower -= 5;
            SyncData();

            await UseCoreAttack();

            _isCoreAttack = false;
            _coreAttackTower.SetCanAttack(_canCoreAttack);
            SyncUI();
        }
        else if(_canCoreAttack == false && _isCoreAttack == false)
        {
            Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetWarning(_musicPowerWarning);
        }
        else if(_isCoreAttack == true)
        {
            Managers.Instance.UI.GetRootUI().GetCanvas<TextCanvas>().SetWarning(_alreadyCoreAttacking);
        }
    }

    public void SyncUI()
    {
        if(_canCoreAttack == false)
        {
            float coreAttackStack = _musicPowerData.CoreMusicPower * 0.2f;
            coreAttackStack = Mathf.Clamp(coreAttackStack, 0f, 1f);
            coreAttackStack = 1 - coreAttackStack;
            DOTween.Kill(_darkScreen);
            _darkScreen.DOFillAmount(coreAttackStack, 0.1f);

            SyncData();
        }
    }

    private void SyncData()
    {
        if (_musicPowerData.CoreMusicPower >= 5)
        {
            _canCoreAttack = true;
        }
        else
        {
            _canCoreAttack = false;
        }
    }

    private async UniTask UseCoreAttack()
    {
        float time = 30f;
        float t = 0f;

        while(t <= time)
        {
            await UniTask.Yield();
            t += Time.deltaTime;

            _darkScreen.fillAmount = t / time;
        }
    }
}
