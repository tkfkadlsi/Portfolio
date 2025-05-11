using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoomString : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boomstring;
    PhaseManager phaseManager;
    string text = "";
    void Start()
    {
        phaseManager = GameManager.instance.phaseManager;
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public IEnumerator CallBoom()
    {
        yield return new WaitForSeconds(1.0f);
        text = "��ĭ�� ����ȭ Ȯ��.";
        boomstring.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            boomstring.text += text[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1.0f);
        text = "���� ������ ���� �����̴�.";
        boomstring.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            boomstring.text += text[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1.0f);
        phaseManager.FinishBoomText();
    }

    public IEnumerator FailBoom()
    {
        yield return new WaitForSeconds(1.0f);
        text = "���� ���� ���� ��...";
        boomstring.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            boomstring.text += text[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1.0f);
        text = "���ݴ��Ѵ�! ���� ���! �Ĥ�...��...��....!";
        boomstring.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            boomstring.text += text[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
