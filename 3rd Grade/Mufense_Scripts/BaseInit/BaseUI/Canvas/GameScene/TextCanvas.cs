using TMPro;
using UnityEngine;

public class TextCanvas : BaseCanvas
{
    enum ETexts
    {
        GameStartText,
        GameTimerText
    }

    private TextMeshProUGUI _gameStartText;
    private TextMeshProUGUI _gameTimerText;

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _gameStartText = Get<TextMeshProUGUI>((int)ETexts.GameStartText);
        _gameTimerText = Get<TextMeshProUGUI>((int)ETexts.GameTimerText);
    }

    public void GameStart()
    {
        _gameStartText.gameObject.SetActive(false);
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

        float time = Mathf.Round(times[shortestTimeIndex] * 10) * 0.1f;

        string text = $"{strings[shortestTimeIndex]} : {time}s";

        _gameTimerText.text = text;
    }

    #endregion
}
