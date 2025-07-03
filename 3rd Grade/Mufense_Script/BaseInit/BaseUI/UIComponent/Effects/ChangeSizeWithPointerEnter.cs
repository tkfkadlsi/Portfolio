using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSizeWithPointerEnter : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _sizeMultiple = 1.1f;

    private Vector3 _originScale;

    protected override void Init()
    {
        base.Init();

        _originScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _originScale * _sizeMultiple;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _originScale;
    }
}
