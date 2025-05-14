using TMPro;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI resumeText;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private TextMeshProUGUI exitText;

    private void OnEnable()
    {
        if(Information.Instance.IsKorean)
        {
            pauseText.text = "�Ͻ�����";
            descriptionText.text = "������ ��� ������.\n�ٽ� �������� ���ư����?";
            resumeText.text = "���ư���";
            restartText.text = "�ٽ��ϱ�";
            exitText.text = "���� ȭ��";
        }
        else
        {
            pauseText.text = "Pause";
            descriptionText.text = "You've paused your adventure for a moment.\nShall we return to the adventure?";
            resumeText.text = "Resume";
            restartText.text = "Restart";
            exitText.text = "Exit Game";
        }
    }
}