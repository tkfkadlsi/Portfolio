using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : BaseUI
{
    enum EClass
    {
        F = 0,
        E = 1,
        D = 2,
        C = 3,
        B = 5,
        A = 8,
        S = 14,
        SS = 20,
        PF = 30,
    }

    enum ETexts
    {
        KillCountText,
        LiveTimeText,
        ClassText
    }

    enum EButtons
    {
        TitleButton
    }

    private TextMeshProUGUI _killCountText;
    private TextMeshProUGUI _liveTimeText;
    private TextMeshProUGUI _classText;

    private Button _titleButton;

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));

        _killCountText = Get<TextMeshProUGUI>((int)ETexts.KillCountText);
        _liveTimeText = Get<TextMeshProUGUI>((int)ETexts.LiveTimeText);
        _classText = Get<TextMeshProUGUI>((int)ETexts.ClassText);

        _titleButton = Get<Button>((int)EButtons.TitleButton);

        _killCountText.text = "";
        _liveTimeText.text = "";
        _classText.text = "";

        _classText.color = Color.clear;
        _classText.rectTransform.localScale = new Vector3(2, 2, 2);
    }

    protected override void Enable()
    {
        base.Enable();
        EClass resultClass = ResultCalculation(Managers.Instance.Game.LiveTime, Managers.Instance.Game.KillCount);
        ResultDisplay(Managers.Instance.Game.LiveTime, Managers.Instance.Game.KillCount, resultClass);

        _titleButton.onClick.AddListener(ButtonHandler);
    }

    protected override void Disable()
    {
        base.Disable();

        _titleButton.onClick.RemoveAllListeners();
    }

    private EClass ResultCalculation(float liveTime, int killCount)
    {
        float liveTimeMinutes = liveTime / 60f;
        int correctionKillCount = (int)((killCount / liveTimeMinutes) - 150f);

        int killValue = (int)(correctionKillCount / 5f);
        int liveValue = (int)(liveTime / 45f);

        int resultValue = killValue + liveValue;

        EClass resultClass = EClass.F;

        foreach(EClass eClass in Enum.GetValues(typeof(EClass)))
        {
            if((int)eClass > resultValue) continue;

            if((int)eClass >= (int)resultClass)
            {
                resultClass = eClass;
            }
        }

        return resultClass;
    }

    private async UniTask ResultDisplay(float liveTime, int killCount, EClass resultClass)
    {
        float time = 2f;
        float t = 0;

        float lerpLiveTime = 0;
        int lerpKillCount = 0;

        while(t < time)
        {
            t += Time.deltaTime;
            await UniTask.Yield();

            lerpLiveTime = Mathf.Lerp(0, liveTime, t / time);
            lerpKillCount = (int)Mathf.Lerp(0, killCount, t / time);

            _liveTimeText.text = $"{(int)(lerpLiveTime / 60f)}:{lerpLiveTime % 60}";
            _killCountText.text = lerpKillCount.ToString();
        }

         string classText = Enum.GetName(typeof(EClass), resultClass);
        _classText.text = classText;

        _classText.DOColor(Color.white, 2f);
        _classText.rectTransform.DOScale(1f, 2f);
    }

    private void ButtonHandler()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }
}
