using System.Collections.Generic;
using System;
using UnityEngine;

public class TitleMusicPlayer : MonoBehaviour
{
    private AudioSource _musicPlayer;
    private MusicDataStorage _musicdata;
    public float MusicPlayTime => _musicPlayer.time;
    private readonly float _musicPlayCooltime = 60f;

    private bool _isPlaying;
    private bool _isStart;

    private float _unitTime;
    public float UnitTime { get { return _unitTime; } }

    private MusicType _playingMusicType;
    public Music PlayingMusic
    {
        get
        {
            if (_musicdata.MusicDictionary.ContainsKey(_playingMusicType) == false)
                return null;

            return _musicdata.MusicDictionary[_playingMusicType];
        }
    }

    private void Awake()
    {
        _musicdata = GetComponent<MusicDataStorage>();
        _musicPlayer = GetComponent<AudioSource>();
        _isPlaying = false;
        _isStart = false;
    }

    private void Start()
    {
        PlayMusic(MusicType.Victory);
    }

    private void PlayMusic(MusicType type, float time = 0f)
    {
        if (PlayingMusic != null)
        {
            PlayingMusic.PlayCoolDown = _musicPlayCooltime;
        }

        if (time == 0f)
        {
            _bpmPlayCount = 0;
            _corePlayCount = 0;
            _beatPlayCount = 0;
            _pianoPlayCount = 0;
            _drumPlayCount = 0;
            _guitarPlayCount = 0;
            _violinPlayCount = 0;
            _trumpetPlayCount = 0;
            _vocalPlayCount = 0;
        }

        _musicPlayer.Stop();

        _playingMusicType = type;
        _musicPlayer.clip = _musicdata.MusicDictionary[type].Clip;
        _musicPlayer.time = time;
        _musicPlayer.Play();
        _isPlaying = true;

        Managers.Instance.Game.MusicPlayEvent?.Invoke();
        _isStart = true;
    }

    #region Update

    private int _bpmPlayCount;

    private int _beatPlayCount;
    private int _corePlayCount;
    private int _pianoPlayCount;
    private int _drumPlayCount;
    private int _guitarPlayCount;
    private int _violinPlayCount;
    private int _trumpetPlayCount;
    private int _vocalPlayCount;

    private void Update()
    {
        CoolDown();
        if (_isPlaying)
        {

            BpmCheck();

            PlayCore();
            PlayBeat();
            if (PlayingMusic.IsPianoUsable) PlayPiano();
            if (PlayingMusic.IsDrumUsable) PlayDrum();
            if (PlayingMusic.IsGuitarUsable) PlayGuitar();
            if (PlayingMusic.IsViolinUsable) PlayViolin();
            if (PlayingMusic.IsTrumpetUsable) PlayTrumpet();
            if (PlayingMusic.IsVocalUsable) PlayVocal();
        }

        if (_isStart == true && _musicPlayer.isPlaying == false)
        {
            Debug.Log("곡 끝남");
            PlayMusic(PlayingMusic.Type);
        }
    }

    private void CoolDown()
    {
        foreach (Music music in _musicdata.MusicDictionary.Values)
        {
            music.PlayCoolDown -= Time.deltaTime;
        }
    }

    private void BpmCheck()
    {
        if (_musicdata.BpmDictionary[_playingMusicType].Count <= _bpmPlayCount) return;

        while (_musicdata.BpmDictionary[_playingMusicType][_bpmPlayCount].Time <= MusicPlayTime)
        {
            _unitTime = 60f / _musicdata.BpmDictionary[_playingMusicType][_bpmPlayCount].Bpm;
            _bpmPlayCount++;

            if (_musicdata.BpmDictionary[_playingMusicType].Count <= _bpmPlayCount)
            {
                break;
            }
        }
    }

    private void PlayCore()
    {
        if (_musicdata.CoreDictionary[_playingMusicType].Count <= _corePlayCount) return;

        while (_musicdata.CoreDictionary[_playingMusicType][_corePlayCount].Timing <= MusicPlayTime)
        {
            Managers.Instance.Game.CorePlayEvent?.Invoke(_musicdata.CoreDictionary[_playingMusicType][_corePlayCount].IsHighNote);
            //Debug.Log($"Core || MusicTime : {_musicPlayTime}, NoteTime : {_musicdata.CoreDictionary[_playingMusicType][_corePlayCount].Timing}");

            _corePlayCount++;

            if (_musicdata.CoreDictionary[_playingMusicType].Count <= _corePlayCount)
            {
                break;
            }
        }
    }

    private void PlayBeat()
    {
        if (_musicdata.BeatDictionary[_playingMusicType].Count <= _beatPlayCount) return;

        while (_musicdata.BeatDictionary[_playingMusicType][_beatPlayCount] <= MusicPlayTime)
        {
            Managers.Instance.Game.BeatEvent?.Invoke();
            //Debug.Log($"Beat || MusicTime : {_musicPlayTime}, NoteTime : {_musicdata.BeatDictionary[_playingMusicType][_beatPlayCount]}");

            _beatPlayCount++;

            if (_musicdata.BeatDictionary[_playingMusicType].Count <= _beatPlayCount)
            {
                break;
            }
        }
    }

    private void PlayPiano()
    {
        PlayInstrument(_playingMusicType, _musicdata.PianoDictionary, ref _pianoPlayCount, (bool isHigh) =>
        {
            Managers.Instance.Game.PianoPlayEvent?.Invoke(isHigh);
        });
    }

    private void PlayDrum()
    {
        PlayInstrument(_playingMusicType, _musicdata.DrumDictionary, ref _drumPlayCount, (bool isHigh) =>
        {
            Managers.Instance.Game.DrumPlayEvent?.Invoke(isHigh);
        });
    }

    private void PlayGuitar()
    {
        PlayInstrument(_playingMusicType, _musicdata.GuitarDictionary, ref _guitarPlayCount, (bool isHigh) =>
        {
            Managers.Instance.Game.GuitarPlayEvent?.Invoke(isHigh);
        });
    }

    private void PlayViolin()
    {
        PlayInstrument(_playingMusicType, _musicdata.ViolinDictionary, ref _violinPlayCount, (bool isHigh) =>
        {
            Managers.Instance.Game.ViolinPlayEvent?.Invoke(isHigh);
        });
    }

    private void PlayTrumpet()
    {
        PlayInstrument(_playingMusicType, _musicdata.TrumpetDictionary, ref _trumpetPlayCount, (bool isHigh) =>
        {
            Managers.Instance.Game.TrumpetPlayEvent?.Invoke(isHigh);
        });
    }

    private void PlayVocal()
    {
        if (_musicdata.VocalDictionary[_playingMusicType].Count <= _vocalPlayCount) return;

        while (_musicdata.VocalDictionary[_playingMusicType][_vocalPlayCount].StartTime <= MusicPlayTime)
        {
            Managers.Instance.Game.VocalPlayEvent?.Invoke(
                _musicdata.VocalDictionary[_playingMusicType][_vocalPlayCount].EndTime
                - //마이너스
                _musicdata.VocalDictionary[_playingMusicType][_vocalPlayCount].StartTime
                );

            _vocalPlayCount++;

            if (_musicdata.VocalDictionary[_playingMusicType].Count <= _vocalPlayCount)
            {
                break;
            }
        }
    }

    private void PlayInstrument(MusicType type, Dictionary<MusicType, List<InstrumentsNote>> dict, ref int playCount, Action<bool> action)
    {
        if (dict[type].Count <= playCount) return;

        while (dict[type][playCount].Timing <= MusicPlayTime)
        {

            if (dict[type].Count <= playCount)
            {
                break;
            }

            action?.Invoke(dict[type][playCount].IsHighNote);
            playCount++;
        }
    }

    #endregion

}
