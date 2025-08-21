using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : BaseUI
{
    enum ETexts
    {
        TypeText,
        LevelText,
        DamageText,
        RangeText,
        InfoText,
        AlreadyUpgradeText
    }

    enum EButtons
    {
        UpgradeButton,
        BreakButton
    }

    private TextMeshProUGUI _typeText;
    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _damageText;
    private TextMeshProUGUI _rangeText;
    private TextMeshProUGUI _infoText;
    private TextMeshProUGUI _alreadyUpgradeText;

    private Button _upgradeButton;
    private Button _breakButton;

    private Tower _focusingTower;

    private readonly string _level = "★";

    private readonly string _damage = "공격력 : ";
    private readonly string _range = "사거리 : ";

    private readonly string _krAlreadyUpgrade = "업그레이드 진행중..";
    private readonly string _krMaxUpgrade = "최고 레벨입니다.";

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        
        _typeText = Get<TextMeshProUGUI>((int)ETexts.TypeText);
        _levelText = Get<TextMeshProUGUI>((int)ETexts.LevelText);
        _damageText = Get<TextMeshProUGUI>((int)ETexts.DamageText);
        _rangeText = Get<TextMeshProUGUI>((int)ETexts.RangeText);
        _infoText = Get<TextMeshProUGUI>((int)ETexts.InfoText);
        _alreadyUpgradeText = Get<TextMeshProUGUI>((int)ETexts.AlreadyUpgradeText);

        _upgradeButton = Get<Button>((int)EButtons.UpgradeButton);
        _breakButton = Get<Button>((int)EButtons.BreakButton);

        _upgradeButton.onClick.AddListener(UpgradeButtonHandler);
        _breakButton.onClick.AddListener(BreakButtonHandler);
    }

    protected override void Release()
    {
        base.Release();

        _upgradeButton.onClick.RemoveAllListeners();
        _breakButton.onClick.RemoveAllListeners();
    }

    public void SetTower(Tower tower)
    {
        _focusingTower = tower;

        _typeText.text = Managers.Instance.Data.ConvertData.TowerType2KRText[_focusingTower.Type];

        SyncUI(_focusingTower);
    }

    public void SyncUI(Tower caller)
    {
        if (caller != _focusingTower) return;

        _levelText.text = "";
        for (int i = 0; i < _focusingTower.Level; i++)
        {
            _levelText.text += _level;
        }

        _damageText.text = _damage + Managers.Instance.Data.TowerStatManagement.GetDamage(caller.Type);
        _rangeText.text = _range + Managers.Instance.Data.TowerStatManagement.GetRange(caller.Type, caller.Level);

        if (_focusingTower.IsUpgrading == true) 
        {
            _infoText.text = Managers.Instance.Data.TowerStatManagement.GetDescription(_focusingTower.Type);
            _upgradeButton.gameObject.SetActive(false);
            _alreadyUpgradeText.gameObject.SetActive(true);
            _alreadyUpgradeText.text = _krAlreadyUpgrade;
        }
        else if (_focusingTower.Level == 3)
        {
            _infoText.text = "";
            _upgradeButton.gameObject.SetActive(false);
            _alreadyUpgradeText.gameObject.SetActive(true);
            _alreadyUpgradeText.text = _krMaxUpgrade;
        }
        else
        {
            _infoText.text = Managers.Instance.Data.TowerStatManagement.GetDescription(_focusingTower.Type);
            _upgradeButton.gameObject.SetActive(true);
            _alreadyUpgradeText.gameObject.SetActive(false);
        }
    }

    private void UpgradeButtonHandler()
    {
        if (_focusingTower.gameObject.activeInHierarchy == true)
        {
            _focusingTower.LevelUpStart();
            Managers.Instance.UI.GetRootUI().GetCanvas<TowerInfoCanvas>().ClosePanel();
        }
    }

    private void BreakButtonHandler()
    {
        if(_focusingTower.gameObject.activeInHierarchy == true)
        {
            _focusingTower.BreakTower();
            Managers.Instance.UI.GetRootUI().GetCanvas<TowerInfoCanvas>().ClosePanel();
        }
    }
}
