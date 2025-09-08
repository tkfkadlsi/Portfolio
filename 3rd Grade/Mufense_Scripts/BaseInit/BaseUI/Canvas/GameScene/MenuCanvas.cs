using UnityEngine;

public class MenuCanvas : BaseCanvas
{
    private OnCoreAttackButton _coreAttackButton;

    protected override void Init()
    {
        base.Init();

        SetEnable(true);

        _coreAttackButton = gameObject.FindChild<OnCoreAttackButton>(null, true);
    }

    public void SyncUI()
    {
        _coreAttackButton.SyncUI();
    }
}
