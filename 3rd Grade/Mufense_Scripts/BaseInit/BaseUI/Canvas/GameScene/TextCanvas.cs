using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextCanvas : BaseCanvas
{
    enum ETexts
    {
        GameStartText,
        GameTimerText,
        WarningText
    }

    private TextMeshProUGUI _gameStartText;
    private TextMeshProUGUI _gameTimerText;
    private TextMeshProUGUI _warningText;

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _gameStartText = Get<TextMeshProUGUI>((int)ETexts.GameStartText);
        _gameTimerText = Get<TextMeshProUGUI>((int)ETexts.GameTimerText);
        _warningText = Get<TextMeshProUGUI>((int)ETexts.WarningText);
    }

    public void GameStart()
    {
        _gameStartText.gameObject.SetActive(false);
    }

    public void SetWarning(string text)
    {
        DOTween.Kill(_warningText);
        _warningText.color = Color.white;
        _warningText.text = text;
        _warningText.DOColor(Color.clear, 1f).SetEase(Ease.InCirc);
    }

    #region Timer

    private float[] times = new float[2];
    private string[] strings = new string[2];

    private int shortestTimeIndex;

    public void SetString(int index, string text)
    {
        strings[index] = text;
    }

    public void SetTime(int index, float time)
    {
        times[index] = time;
    }

    private void Update()
    {
        float shortestTime = float.MaxValue;

        for (int i = 0; i < times.Length; i++)
        {
            if (shortestTime > times[i])
            {
                shortestTime = times[i];
                shortestTimeIndex = i;
            }
        }

        int time = (int)(Mathf.Round(shortestTime * 10f));

        int second = (int)(time * 0.1f);
        int hundredms = ((int)time) % 10;

        string text = $"{strings[shortestTimeIndex]} : {second}.{hundredms}s";

        _gameTimerText.text = text;
    }

    #endregion
}
