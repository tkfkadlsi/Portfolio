using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;

    public void GameStart()
    {
        StartCoroutine(Text1());
    }

    IEnumerator Text1()
    {
        string str = "���� ����ο� ���� ���� ȯ���մϴ�!";
        tutorialText.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text2());
    }

    IEnumerator Text2()
    {
        string str = "Ű ������ ���� �ʾ��� ���� �⺻ Ű�� D, F, J, K�Դϴ�.";
        tutorialText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text3());
    }

    IEnumerator Text3()
    {
        string str = "������ �������� ���� ��� �Դϴ�.";
        tutorialText.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text4());
    }

    IEnumerator Text4()
    {
        string str = "Critical, Nice, Miss�� ������ ��Ȯ���� ���� ������ �������� ���� �� �ֽ��ϴ�.";
        tutorialText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text5());
    }

    IEnumerator Text5()
    {
        string str = "�������� �Ķ����� ��� ����Դϴ�.";
        tutorialText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text6());
    }

    IEnumerator Text6()
    {
        string str = "Defense, Safe, Break�� ������ ��Ȯ���� ���� �������� ����մϴ�.";
        tutorialText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(Text7());
    }

    IEnumerator Text7()
    {
        string str = "�Ƿ��� ������ �� ������ óġ���ּ���! ��Ź�帳�ϴ�!";
        tutorialText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialText.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);
    }
}
