using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private TextMeshProUGUI currentLanguageText;
    [SerializeField] private TextMeshProUGUI bookTextKor;
    [SerializeField] private TextMeshProUGUI bookTextEng;
    KeySetting keySetting;
    public void SettingEnterButton()
    {
        settingUI.SetActive(true);
    }
    public void SettingExitButton()
    {
        settingUI.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void KeyModeChangeButton(string keyMode)
    {
        keySetting._Keymode = keyMode;
        if(keyMode == "Keytar")
        {
            keySetting.SetKeytar();
        }
        else if(keyMode == "Keyboard")
        {
            keySetting.SetKeyboard();
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void BookOpen()
    {
        if(Information.Instance.Language == "�ѱ���")
        {
            bookTextEng.enabled = false;
            bookTextKor.enabled = true;
        }
        else if(Information.Instance.Language == "English")
        {
            bookTextKor.enabled = false;
            bookTextEng.enabled = true;
        }

        book.SetActive(true);
    }
    public void BookClose()
    {
        book.SetActive(false);
    }

    public void LanguageChange()
    {
        if(Information.Instance.Language == "�ѱ���")
        {
            Information.Instance.Language = "English";
            currentLanguageText.text = "English";
            PlayerPrefs.SetString("Language", "English");
        }
        else
        {
            Information.Instance.Language = "�ѱ���";
            currentLanguageText.text = "�ѱ���";
            PlayerPrefs.SetString("Language", "�ѱ���");
        }

        restartPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }



    private void Awake()
    {
        settingUI.SetActive(false);
        keySetting = FindObjectOfType<KeySetting>();

        int w = Screen.width;
        int h = w * 9 / 16;

        Screen.SetResolution(w, h, true);
        restartPanel.SetActive(false);
    }

    private void Start()
    {
        currentLanguageText.text = Information.Instance.Language;
    }
}
