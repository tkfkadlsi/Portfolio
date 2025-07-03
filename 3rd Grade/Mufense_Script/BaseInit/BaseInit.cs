using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public abstract class BaseInit : MonoBehaviour
{
    protected bool _init { get; private set; } = false;
    protected int _enabled { get; private set; } = 0;
    protected int _disabled { get; private set; } = 0;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void OnDestroy()
    {
        Release();
    }

    protected virtual void Init()
    {
        _init = true;
    }

    protected virtual void Enable()
    {
        _enabled++;
    }

    protected virtual void Disable()
    {
        _disabled++;
    }

    protected virtual void Release()
    {

    }
}
