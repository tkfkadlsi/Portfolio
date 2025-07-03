using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public abstract class BaseButton : BaseUI
{
    protected Button _button;

    protected override void Init()
    {
        base.Init();

        _button = GetComponent<Button>();

        _button.onClick.AddListener(ButtonHandler);
    }

    protected override void Release()
    {
        base.Release();

        _button.onClick.RemoveAllListeners();
    }

    protected abstract void ButtonHandler();
}
