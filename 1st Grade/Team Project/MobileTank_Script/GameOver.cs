using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image panelalpha;
    [SerializeField] private GameObject stringPanel;
    [SerializeField] private GameObject bossLArm;
    [SerializeField] private GameObject boss;
    [SerializeField] private SpriteRenderer LArmRenderer;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI stringText;
    [SerializeField] private Image image;
    [SerializeField] private GameObject overText;
    private IEnumerator Start()
    {
        stringPanel.SetActive(false);
        overText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.05f);
            panelalpha.color = new Color(1, 1, 1, panelalpha.color.a - 0.1f);
        }

        yield return new WaitForSeconds(2.0f);

        nameText.text = "";
        stringText.text = "";
        image.enabled = false;
        string name;
        string text;

        name = "������";
        text = "���������.";
        stringPanel.SetActive(true);
        nameText.text = name;
        for(int i = 0; i < text.Length; i++)
        {
            stringText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        nameText.text = "";
        stringText.text = "";
        name = "�츶��";
        text = ".....";
        image.enabled = true;
        nameText.text = name;
        for(int i = 0; i < text.Length; i++)
        {
            stringText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        nameText.text = "";
        stringText.text = "";
        name = "������";
        text = "�׾��, ����. ������ �˴� �������� �����ްڴ�.";
        image.enabled = false;
        nameText.text = name;
        for(int i = 0; i < text.Length; i++)
        {
            stringText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2.0f);

        stringPanel.SetActive(false);

        bossLArm.transform.DORotate(new Vector3(0, 0, 100), 2.0f);
        boss.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2.0f);
        boss.transform.DOMove(new Vector3(0, boss.transform.position.y - 1.0f), 2.0f);
        yield return new WaitForSeconds(0.5f);
        LArmRenderer.sortingLayerName = "BossArm";
        yield return new WaitForSeconds(2.5f);

        bossLArm.transform.DORotate(new Vector3(0, 0, 45), 0.05f).OnComplete(()=>
        {
            panelalpha.color = new Color(0, 0, 0, 1);
        });
        yield return new WaitForSeconds(1.5f);
        overText.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.P));
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
