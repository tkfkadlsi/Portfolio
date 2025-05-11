using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI songInfoText;

    public void SelectStage1()
    {
        Information.instance.Stage = 1;
        songInfoText.text = "title : smooooch A\n\nbpm : 177\n\ndifficult : 4/ 10";
    }

    public void SelectStage2()
    {
        Information.instance.Stage = 2;
        songInfoText.text = "title : Back In My Days\n\nbpm:118\n\ndifficult : 2 / 10";
    }

    public void SelectStage3()
    {
        Information.instance.Stage = 3;
        songInfoText.text = "title : In 59 second\n\nbpm:200\n\ndifficult : 6 / 10\n\n���� : ä���� ���� �������ϴ�. �ʹ� �̻��� ä���ΰ� �дϴ�.\n�÷��̸� �ﰡ�Ͻñ� �ٶ��ϴ�.";
    }

    public void SelectStage4()
    {
        Information.instance.Stage = 4;
        songInfoText.text = "title : Freedom Dive (T+pazolite remix)\n\nbpm:234.5\n\ndifficult : 8 / 10";
    }

    public void SelectStage5()
    {
        Information.instance.Stage = 5;
        StageStart();
    }

    public void StageStart()
    {
        SceneManager.LoadScene(2);
    }
}
