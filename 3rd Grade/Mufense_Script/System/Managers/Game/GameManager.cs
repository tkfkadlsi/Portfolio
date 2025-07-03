using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region WaitForSecond

    private Dictionary<float, WaitForSeconds> _waitForSecondDictionary = new Dictionary<float, WaitForSeconds>();

    public WaitForSeconds GetWaitForSeconds(float seconds)
    {
        if(!_waitForSecondDictionary.ContainsKey(seconds))
        {
            _waitForSecondDictionary.Add(seconds, new WaitForSeconds(seconds));
        }

        return _waitForSecondDictionary[seconds];
    }

    #endregion

    #region Component

    private Dictionary<Type, MonoBehaviour> _componentDictionary = new Dictionary<Type, MonoBehaviour>();

    public T GetComponentInScene<T>() where T : MonoBehaviour
    {
        Type type = typeof(T);

        if(!_componentDictionary.ContainsKey(type))
        {
            _componentDictionary.Add(type, FindAnyObjectByType<T>());
        }
        else if(_componentDictionary[type] == null)
        {
            _componentDictionary[type] = FindAnyObjectByType<T>();
        }

        return _componentDictionary[type] as T;
    }

    #endregion

    #region Event

    public Action BeatEvent;
    public Action<bool> CorePlayEvent;
    public Action<bool> PianoPlayEvent;
    public Action<bool> DrumPlayEvent;
    public Action<bool> GuitarPlayEvent;
    public Action<bool> ViolinPlayEvent;
    public Action<bool> TrumpetPlayEvent;
    public Action<float> VocalPlayEvent;

    public Action TowerChangeEvent;

    public void ClearEvents()
    {
        BeatEvent = null;
        CorePlayEvent = null;
        PianoPlayEvent = null;
        DrumPlayEvent = null;
        GuitarPlayEvent = null;
        ViolinPlayEvent = null;
        TrumpetPlayEvent = null;
        VocalPlayEvent = null;
        TowerChangeEvent = null;
    }

    #endregion

    #region Music

    public Music PlayingMusic => GetComponentInScene<MusicPlayer>().PlayingMusic;

    public float UnitTime => GetComponentInScene<MusicPlayer>().UnitTime;

    #endregion

    public void AllClear()
    {
        _waitForSecondDictionary.Clear();
        _componentDictionary.Clear();
        ClearEvents();
    }

    private void OnApplicationQuit()
    {
        AllClear();
    }

    private void OnDestroy()
    {
        AllClear();
    }
}
