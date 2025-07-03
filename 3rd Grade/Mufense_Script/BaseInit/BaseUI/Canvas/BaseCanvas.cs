using UnityEngine;

public class BaseCanvas : BaseUI
{
    private Canvas _canvas;

    protected override void Init()
    {
        base.Init();

        _canvas = GetComponent<Canvas>();
    }

    public void SetEnable(bool enable)
    {
        _canvas.enabled = enable;
    }

    public bool GetEnable()
    {
        return _canvas.enabled;
    }
}
