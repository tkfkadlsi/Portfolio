using System;
using System.Collections.Generic;
using UnityEngine;

public class RootUI : BaseInit
{
    private Dictionary<Type, BaseCanvas>  _baseCanvasDictionary = new Dictionary<Type, BaseCanvas>();

    protected override void Init()
    {
        base.Init();

        BaseCanvas[] baseCanvases = GetComponentsInChildren<BaseCanvas>();

        foreach (BaseCanvas canvas in baseCanvases)
        {
            Type type = canvas.GetType();
            if (!_baseCanvasDictionary.ContainsKey(type))
            {
                _baseCanvasDictionary.Add(type, canvas);
            }
        }
    }

    protected override void Enable()
    {
        base.Enable();

        Managers.Instance.UI.SetRootUI(this);
    }

    protected override void Disable()
    {
        base.Disable();

        if(Managers.Instance != null)
        {
            Managers.Instance.UI.SetRootUI(null);
        }
    }

    public T GetCanvas<T>() where T : BaseCanvas
    {
        if(_baseCanvasDictionary.ContainsKey(typeof(T)))
        {
            return _baseCanvasDictionary[typeof(T)] as T;
        }
        else
        {
            Debug.LogError($"[사람이냐] {typeof(T).Name}은 없는 Canvas 입니다.");
            return null;
        }
    }
}
