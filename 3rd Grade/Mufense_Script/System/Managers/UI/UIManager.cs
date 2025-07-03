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
            Debug.LogError("현재 씬에는 RootUI가 없습니다.");
            return null;
        }
        
        return _rootUI;
    }
}
