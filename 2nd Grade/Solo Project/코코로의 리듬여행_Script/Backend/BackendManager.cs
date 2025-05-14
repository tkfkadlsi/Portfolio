using BackEnd;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackendManager : MonoBehaviour
{
    public static BackendManager Instance;

    private Canvas canvas;
    [SerializeField] private TextMeshProUGUI errmsg;

    private bool IsAwake = true;

    private void Awake()
    {
        Backend.Initialize(true);
        IsAwake = false;

        Instance = this;

        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    public void ErrorMsg(string msg)
    {
        errmsg.text = msg;
        canvas.enabled = true;
    }

    public void CloseError()
    {
        canvas.enabled = false;
    }

    async void Test()
    {

        await Task.Run(() =>
        {
            //CustomSignUp("tkfkadlsi", "@rladPtjd070113");
            //CustomLogin("tkfkadlsi", "@rladPtjd070113");
            //UpdateNickname("tkfkadlsi");
            //GameDataGet();
            //AureliaRank();

            Debug.Log("�׽�Ʈ�� �����մϴ�");
        });
    }

    public void CustomSignUp(string id, string pw)
    {
        Debug.Log("ȸ�������� ��û�մϴ�.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�. : " + bro);
            UpdateNickname(id);
            GameDataInsert();
            SceneManager.LoadScene("4_Tutorial");
        }
        else
        {
            var errcode = bro.GetStatusCode();
            switch (errcode)
            {
                case "409":
                    ErrorMsg("�ߺ��� ���̵𿡿�.");
                    break;
                case "401":
                    ErrorMsg("�������̿���.");
                    break;
                default:
                    ErrorMsg("�� �� ���� ��������.");
                    break;
            }
        }
    }

    public void CustomLogin(string id, string pw)
    {
        Debug.Log("�α����� ��û�մϴ�.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α��ο� �����߽��ϴ�.");
            GetUserNickName();
            GameDataGet();
            GetRanks();
            UIManager.Instance.ProfileFuncs.SetProfile();
            TitleUIManager.Instance.LoginSuccess();
        }
        else
        {
            var errorcode = bro.GetStatusCode();
            switch (errorcode)
            {
                case "401":
                    ErrorMsg("���̵� �Ǵ� ��й�ȣ�� Ȯ�����ּ���.");
                    break;
                default:
                    ErrorMsg("�� �� ���� ��������.");
                    break;
            }
        }
    }

    public void GetUserNickName()
    {
        Debug.Log("�г����� �ҷ��ɴϴ�.");
        BackendReturnObject bro = Backend.BMember.GetUserInfo();
        Information.Instance.userNickname = bro.GetReturnValuetoJSON()["row"]["nickname"].ToString();
        Debug.Log("�г��� �ҷ����⸦ �����߽��ϴ�.");
    }

    public void UpdateNickname(string nickname)
    {
        Debug.Log("�г��� ������ ��û�մϴ�.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("�г��� ���濡 �����߽��ϴ� : " + bro);
        }
        else
        {
            Debug.LogError("�г��� ���濡 �����߽��ϴ� : " + bro);
        }
    }

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert()
    {
        Param param = new Param();
        param.Add("Playcount", Information.Instance.GameData.playCount);
        param.Add("HighScore", Information.Instance.GameData.PlayerHighScore);
        param.Add("HighRate", Information.Instance.GameData.PlayerHighRate);

        Backend.GameData.Insert("GameData", param);
    }

    public void GameDataGet()
    {
        Debug.Log("���� ���� ��ȸ �Լ��� ȣ���մϴ�.");
        var bro = Backend.GameData.GetMyData("GameData", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json���� ���ϵ� �����͸� �޾ƿɴϴ�.

            // �޾ƿ� �������� ������ 0�̶�� �����Ͱ� �������� �ʴ� ���Դϴ�.
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //�ҷ��� ���������� �������Դϴ�.
                Information.Instance.GameData = new GameData();

                Information.Instance.GameData.playCount = int.Parse(gameDataJson[0]["Playcount"].ToString());

                foreach (string key in gameDataJson[0]["HighScore"].Keys)
                {
                    Debug.Log(key);
                    Information.Instance.GameData.PlayerHighScore.Add(key, int.Parse(gameDataJson[0]["HighScore"][key].ToString()));
                }
                foreach (string key in gameDataJson[0]["HighRate"].Keys)
                {
                    Information.Instance.GameData.PlayerHighRate.Add(key, float.Parse(gameDataJson[0]["HighRate"][key].ToString()));
                }

            }
        }
        else
        {
            Debug.LogError("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);
        }
    }

    public void GameDataUpdate()
    {
        if (Information.Instance.GameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param();
        param.Add("Playcount", Information.Instance.GameData.playCount);
        param.Add("HighScore", Information.Instance.GameData.PlayerHighScore);
        param.Add("HighRate", Information.Instance.GameData.PlayerHighRate);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.Update("GameData", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("GameData", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }

    public void GetRanks()
    {
        string aureliaUUID = "3d644d20-1c11-11ef-be9a-ff517081b521";
        string witchRecordUUID = "503e59e0-1c11-11ef-8481-0d388203abc3";
        var aureliaBro = Backend.URank.User.GetRankList(aureliaUUID);
        var witchRecordBro = Backend.URank.User.GetRankList(witchRecordUUID);

        if (!aureliaBro.IsSuccess() || !witchRecordBro.IsSuccess())
        {
            Debug.Log("��ŷ ��ȸ ����");
            return;
        }

        List<RankData> aureliaRanks = new List<RankData>();
        List<RankData> witchReocrdRanks = new List<RankData>();

        foreach (LitJson.JsonData jsonData in aureliaBro.FlattenRows())
        {
            RankData rankData = new RankData();

            rankData.nickname = jsonData["nickname"].ToString();
            rankData.score = int.Parse(jsonData["score"].ToString());
            rankData.rank = int.Parse(jsonData["rank"].ToString());

            aureliaRanks.Add(rankData);
        }
        foreach (LitJson.JsonData jsonData in witchRecordBro.FlattenRows())
        {
            RankData rankData = new RankData();

            rankData.nickname = jsonData["nickname"].ToString();
            rankData.score = int.Parse(jsonData["score"].ToString());
            rankData.rank = int.Parse(jsonData["rank"].ToString());

            witchReocrdRanks.Add(rankData);
        }

        Information.Instance.AureliaRanking = aureliaRanks.OrderBy(rank => rank.rank).ToList();
        Information.Instance.WitchRecordRanking = witchReocrdRanks.OrderBy(rank => rank.rank).ToList();
        Debug.Log("��ŷ ��ȸ�� �����߽��ϴ�.");
    }

    public void AureliaRank()
    {
        string rankUUID = "3d644d20-1c11-11ef-be9a-ff517081b521";
        string tableName = "GameData";
        string rowInDate = string.Empty;

        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�. ������ ������ �õ��մϴ�.");
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("������ ���� �� ������ �߻��߽��ϴ� : " + bro2);
                return;
            }

            Debug.Log("������ ���Կ� �����߽��ϴ� : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        string key = DataKey.ReturnKey(SongTitle.Aurelia, DiffcultType.Special);

        if (!Information.Instance.GameData.PlayerHighScore.ContainsKey(key))
        {
            Information.Instance.GameData.PlayerHighScore.Add(key, 0);
        }
        if (!Information.Instance.GameData.PlayerHighRate.ContainsKey(key))
        {
            Information.Instance.GameData.PlayerHighRate.Add(key, 0);
        }

        Param param = new Param();
        param.Add("Playcount", Information.Instance.GameData.PlayerHighScore[key]);

        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��� �� ������ �߻��߽��ϴ�. : " + rankBro);
            return;
        }

        Debug.Log("��ŷ ���Կ� �����߽��ϴ�. : " + rankBro);
    }

    public void WRRank()
    {
        string rankUUID = "503e59e0-1c11-11ef-8481-0d388203abc3";
        string tableName = "GameData";
        string rowInDate = string.Empty;

        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�. ������ ������ �õ��մϴ�.");
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("������ ���� �� ������ �߻��߽��ϴ� : " + bro2);
                return;
            }

            Debug.Log("������ ���Կ� �����߽��ϴ� : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        string key = DataKey.ReturnKey(SongTitle.Witch_Record, DiffcultType.Special);

        if (!Information.Instance.GameData.PlayerHighScore.ContainsKey(key))
        {
            Information.Instance.GameData.PlayerHighScore.Add(key, 0);
        }
        if (!Information.Instance.GameData.PlayerHighRate.ContainsKey(key))
        {
            Information.Instance.GameData.PlayerHighRate.Add(key, 0);
        }

        Param param = new Param();
        param.Add("Playcount", Information.Instance.GameData.PlayerHighScore[key]);

        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��� �� ������ �߻��߽��ϴ�. : " + rankBro);
            return;
        }

        Debug.Log("��ŷ ���Կ� �����߽��ϴ�. : " + rankBro);
    }

    private void OnApplicationQuit()
    {
        GameDataUpdate();
    }
}