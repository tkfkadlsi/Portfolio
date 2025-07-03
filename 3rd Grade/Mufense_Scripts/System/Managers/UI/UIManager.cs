using UnityEngine;

public class UIManager : MonoBehaviour
{
    private RootUI _rootUI;

    public void SetRootUI(RootUI rootUI)
    {
        _rootUI = rootUI;
    }

    public RootUI GetRootUI()
    {
        if(_rootUI == null)
        {
            Debug.LogError("���� ������ RootUI�� �����ϴ�.");
            return null;
        }
        
        return _rootUI;
    }
}
