using DG.Tweening;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    public static Managers Instance
    {
        get
        { 
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Game = GetComponent<GameManager>();
            Pool = GetComponent<PoolManager>();
            Data = GetComponent<DataManager>();
            UI = GetComponent<UIManager>();
            DontDestroyOnLoad(gameObject);

            DOTween.SetTweensCapacity(2000, 500);
            Application.targetFrameRate = -1;
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameManager Game { get; private set; }
    public PoolManager Pool { get; private set; }
    public DataManager Data { get; private set; }
    public UIManager UI { get; private set; }
}
