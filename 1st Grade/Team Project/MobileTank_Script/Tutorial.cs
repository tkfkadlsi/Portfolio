using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    TextMeshProUGUI tutorialString;
    TankMove tankMove;

    string str = "";

    private void Awake()
    {
        tutorialString = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        tankMove = GameManager.instance.tankMove;
        tankMove.enabled = false;
        StartCoroutine("TutorialProgress");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(3);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator TutorialProgress()
    {
        str = "�⺻���� Ʃ�丮���� �����մϴ�. ��ŵ�Ͻ÷��� p�� �����ּ���.";
        tutorialString.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }

        yield return new WaitForSeconds(1f);

        str = "Ʃ�丮���� ������ �����ؾ� �������� �Ѿ�ϴ�.";
        tutorialString.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }

        yield return new WaitForSeconds(1f);

        str = "W�� S�� ����� �յڷ� ������ �� �ֽ��ϴ�.";
        tutorialString.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S));

        yield return new WaitForSeconds(1f);
        tankMove.enabled = false;

        str = "A�� D�� ����� ������ ��ȯ �� �� �ֽ��ϴ�.";
        tutorialString.text = "";
        for(int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.D));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));

        yield return new WaitForSeconds(1f);
        tankMove.enabled = false;

        str = "�¿� ȭ��ǥ�� �̿��� ������ ������ ��ȯ �� �� �ֽ��ϴ�.";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow));

        yield return new WaitForSeconds(1f);
        tankMove.enabled = false;

        str = "��shift�� ���� ��ø� ����� �� �ֽ��ϴ�.";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));

        yield return new WaitForSeconds(1f);
        tankMove.enabled = false;

        str = "F�� ���� ������ �߻��� �� �ֽ��ϴ�.";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));

        yield return new WaitForSeconds(1f);
        tankMove.enabled = false;

        str = "�����̽��� Ű�ٿ��Ͽ� ������ ������ �غ��մϴ�.";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }
        tankMove.enabled = true;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        yield return new WaitForSeconds(0.5f);

        str = "�����̽��� �� ������ ������ �߻��մϴ�.";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));

        str = "���ϼ̽��ϴ�. �⺻���� Ʃ�丮���� ��ġ�ڽ��ϴ�";
        tutorialString.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            tutorialString.text += str[i];
            yield return new WaitForSeconds(0.0625f);
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }
}