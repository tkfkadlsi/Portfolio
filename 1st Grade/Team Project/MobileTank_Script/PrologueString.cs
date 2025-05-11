using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PrologueString : MonoBehaviour
{

    TextMeshProUGUI prt;
    [SerializeField] private GameObject pr;
    [SerializeField] private GameObject StoryText;
    TextMeshProUGUI Storytelling;
    [SerializeField] private GameObject NameText;
    TextMeshProUGUI Nametelling;
    public GameObject che;
    [SerializeField] private GameObject bi;
    public int speed=3;
    string pry;
    string sty;
    string nm;

    private void Awake()
    {
        che.SetActive(false);
    }
    void Start()
    {
        prt = pr.GetComponent<TextMeshProUGUI>();
        Storytelling = StoryText.GetComponent<TextMeshProUGUI>();
        Nametelling = NameText.GetComponent<TextMeshProUGUI>();

        StartCoroutine("PR");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator PR()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "�η��� ������ ����\n ���� �̹��� �������� ��� ���ʳ�";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "�㳪 ���ַ� ������ �η�,\n �����̽����̵�鿡 ���� ��ӵǴ�\n������ ��� �� ����";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "�����̽����̵����\n �׿���� ������ �����ϱ⿡ �̸���.";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "�׿���ߴ� ����\n�ڽŵ��� ������ �����߰�,\n�ᱹ ����������� ������ ���۵ȴ�.";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "������ 2�Ⱓ ��ӵǸ�\n������ ȯ�濡 �ִ�\n�׿������ �л��� £������ ����.";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "�׸���\n�й谡 ������ �׿���ߴ�\n ������ �߾��� �̾��.";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        prt.text = "";
        pry = "���� �߹� 2�� 5���� ���";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.7f);

        //prt.text = "";
        pry = "\n\n-���� ���� �ٹ�";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }

        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        //yield return new WaitForSeconds(2);
        bi.SetActive(false);
        pr.SetActive(false);
        StartCoroutine("Text");
    }
    IEnumerator Text()
    {
        nm = "���� ������ ������";
        sty = "�� ��Ȳ�� ���?";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���決 ����";
        sty = "���� �� 4 �����Դ밡 ���� ���� �������� �� ������ 3�迡 �ش��ϴ� �Դ븦 �̲��� ���� �ֽ��ϴ�.";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }



        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���� ������ ������";
        sty = "�̰����� ��� ���̱�...";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���決 ����";
        sty = "....";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.4f);
        nm = "���決 ����";
        sty = "..? �������? ��..";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }


        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���� ������ ������";
        sty = "���� ���ΰ�?";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }



        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���決 ����";
        sty = "�� 4�Դ밡 ������ ���� ������������ 0.4 AU���� ���̶�� �մϴ�.";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        nm = "���� ������ ������";
        sty = "��? ���� ������ �и� ��� ���� �Ǿ����ٵ�..! �������ΰ�!";
        Storytelling.text = "";
        Nametelling.text = "";
        for (int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        Storytelling.text = "";
        Nametelling.text = "";
        bi.SetActive(true);
        pr.SetActive(true);
       
        prt.text = "";
        pry = "�ᱹ ������ ������ �� ���ߴ� ������ �׿������ �¸��� ������\n�׿������ ��������, �����\n������ ���ϵǾ���.";

        for (int i = 0; i < pry.Length; i++)
        {
            prt.text += pry[i];
            yield return new WaitForSeconds(0.05f);
        }


        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        bi.SetActive(false);
        pr.SetActive(false);


        che.SetActive(true);
        while(che.transform.position.x >5.02)
        {
            yield return new WaitForSeconds(0.002f);
           che. transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        Storytelling.text = "";
        Nametelling.text = "";

        nm = "ü�����";
        sty = "�ݰ��� �츶�� ����";
        for(int i = 0; i < nm.Length; i++)
        {
            Nametelling.text += nm[i];
            yield return new WaitForSeconds(0.05f);
        }
        for(int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }
        //���2
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        Storytelling.text = "";
        nm = "ü�����";
        sty = "�̹� �˰� �ְڴٸ�, ������ ������ ��� �������� ���Ͽ��ٳ�.";
  
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }
        //3
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        Storytelling.text = "";
        nm = "ü�����";
        sty = "���� �������� ���� �������� ��� ��� �Ǿ�����, ���� ����Ͽ� ����� �����̳��� �������ְԳ�";
        
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }
        //4
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        Storytelling.text = "";
        
        nm = "ü�����";
        sty = "���� ���� ������ ������ ���ɼ�, ������ ���.";
       
        for (int i = 0; i < sty.Length; i++)
        {
            Storytelling.text += sty[i];
            yield return new WaitForSeconds(0.05f);
        }
        //�� ��ȯ
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        SceneManager.LoadScene(2);
    }
}
