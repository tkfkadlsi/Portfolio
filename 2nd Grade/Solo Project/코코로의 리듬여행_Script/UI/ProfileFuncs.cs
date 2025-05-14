using TMPro;
using UnityEngine;

public class ProfileFuncs : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nicknameTMP;
    [SerializeField] private TextMeshProUGUI playCountTMP;

    public void SetProfile()
    {
        nicknameTMP.text = Information.Instance.userNickname;
        playCountTMP.text = "�÷��� Ƚ�� : " + Information.Instance.GameData.playCount.ToString();
    }
}
