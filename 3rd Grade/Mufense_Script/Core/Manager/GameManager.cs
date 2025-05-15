using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public InputReaderSO InputReader;

    #region Game

    public float PlayTime;

    #endregion

    #region Music
    public Music PlayingMusic => FindBaseInitScript<MusicPlayer>().PlayingMusic;

    public float CurrentBPM = 120f;
    public float UnitTime => 60f / CurrentBPM;

    public void SetBPM(float bpm)
    {
        CurrentBPM = bpm;
    }

    #endregion

    #region Function

    private Dictionary<Type, BaseInit> _scriptDict = new Dictionary<Type, BaseInit>();

    public T FindBaseInitScript<T>() where T : BaseInit
    {
        if (_scriptDict.ContainsKey(typeof(T)) == false)
        {
            _scriptDict.Add(typeof(T), FindAnyObjectByType<T>());
        }

        return _scriptDict[typeof(T)] as T;
    }

    public void ClearDictionary()
    {
        _scriptDict.Clear();
    }

    private Dictionary<float, WaitForSeconds> _waitDict = new Dictionary<float, WaitForSeconds>();

    public WaitForSeconds GetWaitForSecond(float seconds)
    {
        if(_waitDict.ContainsKey(seconds) == false)
        {
            _waitDict.Add(seconds, new WaitForSeconds(seconds));
        }

        return _waitDict[seconds];
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        FindBaseInitScript<MusicPlayer>().SetPitch(timeScale);
    }

    #endregion

    #region Events

    public Action<Language> ChangeLanguageEvent;

    #endregion

    #region Option

    public Language Language;

    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolume;

    public bool FrameLimit;
    public bool LowDetailMod;

    #endregion
}
