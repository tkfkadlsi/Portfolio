using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class EnemyVisual : BaseObject
{
    [SerializeField] private Material _blackMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _greenMaterial;

    private SkinnedMeshRenderer _renderer;

    protected override void Init()
    {
        base.Init();

        _renderer = GetT<SkinnedMeshRenderer>();
    }

    public void SetVisualColor(EffectType type)
    {
        switch (type)
        {
            case EffectType.None:
                {
                    _renderer.materials[0] = _blackMaterial;
                }
                break;
            case EffectType.HighSpeed:
                {
                    _renderer.materials[0] = _redMaterial;
                }
                break;
            case EffectType.Defend:
                {
                    _renderer.materials[0] = _blueMaterial;
                }
                break;
            case EffectType.Heal:
                {
                    _renderer.materials[0] = _greenMaterial;
                }
                break;
        }
    }
}
