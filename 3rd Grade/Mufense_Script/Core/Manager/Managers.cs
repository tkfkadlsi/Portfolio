
using UnityEngine;

public class Managers : BaseInit
{
    private static Managers instance;   //Managers Ŭ������ ������ �ν��Ͻ�

    public static Managers Instance     //�� �����ϴ� public ������Ƽ
    {
        get
        {
            if (instance == null)   //���� instance�� null�̸�
                instance = FindAnyObjectByType<Managers>(); //���� ������ �ϳ� ã�ƿ���.

            return instance;    //instance ��ȯ
        }
    }

    #region �Ŵ��� ��ũ��Ʈ
    private GameManager _gameManager;
    private PoolManager _poolManager;
    private UIManager _uiManager;
    #endregion

    #region �Ŵ��� ���ٿ� ������Ƽ
    public GameManager Game { get { return _gameManager; } }
    public PoolManager Pool { get { return _poolManager; } }
    public UIManager UI { get { return _uiManager; } }
    #endregion

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Application.targetFrameRate = -1;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = GetComponent<GameManager>();
        _poolManager = GetComponent<PoolManager>();
        _uiManager = GetComponent<UIManager>();

        _poolManager.Init();

        return true;
    }

}
