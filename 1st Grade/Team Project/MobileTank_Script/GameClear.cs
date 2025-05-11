using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClear : MonoBehaviour
{
    [SerializeField] private BossDie bossDie;
    [SerializeField] private Image panelalpha;
    [SerializeField] private TextMeshProUGUI stringText;
    [SerializeField] private GameObject tank;
    [SerializeField] private SpriteRenderer bossEye;
    [SerializeField] private GameObject panel2;
    [SerializeField] private TextMeshProUGUI nameText2;
    [SerializeField] private TextMeshProUGUI stringText2;
    [SerializeField] private Image image;
    [SerializeField] private GameObject clearText;

    string utext = ""; 

    private IEnumerator Start()
    {
        clearText.SetActive(false);
        stringText.enabled = false;
        panel2.SetActive(false);
        image.enabled = false;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.05f);
            panelalpha.color = new Color(1, 1, 1, panelalpha.color.a - 0.1f);
        }

        bossDie.StartCoroutine("Bossexplosion");
        bossDie.StartCoroutine("BossDown");

        yield return new WaitForSeconds(2.5f);

        panelalpha.color = new Color(0, 0, 0, 1);
        stringText.enabled = true;
        stringText.text = "";
        StartCoroutine("Text");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator Text()
    {
        stringText.text = "";
        utext = "�츶�� ������ �׿������ ����� �ı��� ��,\n�׿���߿��Դ� � ���ܵ� ���� �ʰ� �Ǿ���.";

        for(int i = 0; i < utext.Length; i++)
        {
            stringText.text += utext[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stringText.text = "";
        utext = "������ �߾Ǹ��� ������ �׿���ߴ�\n�������� ���ط� ������� �Ǿ���.";

        for (int i = 0; i < utext.Length; i++)
        {
            stringText.text += utext[i];
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stringText.text = "";
        utext = "�׸��� ������ ���� ��,\n������ ��ǿ� �����ϰ� ���� ��...";

        for (int i = 0; i < utext.Length; i++)
        {
            stringText.text += utext[i];
            yield return new WaitForSeconds(0.025f);
        }
        tank.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stringText.enabled = false;
        for(int i = 0; i < 10; i++)
            panelalpha.color = new Color(1, 1, 1, panelalpha.color.a - 0.1f);

        yield return new WaitForSeconds(1.5f);
        bossEye.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        bossEye.color = new Color(0.2f, 0.2f, 0.2f);
        yield return new WaitForSeconds(1.0f);
        bossEye.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        bossEye.color = new Color(0.2f, 0.2f, 0.2f);

        yield return new WaitForSeconds(2.0f);
        for(int i = 0; i < 80; i++)
        {
            bossEye.color = new Color(bossEye.color.r + 0.01f, bossEye.color.g + 0.01f, bossEye.color.b + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1.5f);

        panel2.SetActive(true);
        image.enabled = true;
        nameText2.text = "???";
        stringText2.text = "";

        string text = "���� ������ �ʾҾ�.. ������.. ������߸� ��.";

        for(int i = 0; i < text.Length; i++)
        {
            stringText2.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);
        panelalpha.color = new Color(0, 0, 0, 0);
        for (int i = 0; i < 10; i++)
        {
            panelalpha.color = new Color(0, 0, 0, panelalpha.color.a + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        panel2.SetActive(false);
        image.enabled = false;

        yield return new WaitForSeconds(1.5f);

        clearText.SetActive(true);
    }
}
