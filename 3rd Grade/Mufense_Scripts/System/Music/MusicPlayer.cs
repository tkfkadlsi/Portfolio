using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MusicDataStorage))]
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource _musicPlayer;
    private MusicDataStorage _musicdata;
    public float MusicPlayTime => _musicPlayer.time;
    private readonly float _musicPlayCooltime = 60f;

    private bool _isPlaying;
    private bool _isStart;

    private void Awake()
    {
        _musicdata = GetComponent<MusicDataStorage>();
        _musicPlayer = GetComponent<AudioSource>();
        _isPlaying = false;
        _isStart = false;
    }

    private void Start()
    {
        List<Music> musics = _musicdata.MusicDictionary.Values.ToList();
        for (int i = 0; i < 2; i++)
        {
            Music randomMusic = musics[UnityEngine.Random.Range(0, musics.Count)];
            CardType card = Managers.Instance.Data.ConvertData.MusicType2MusicCardType[randomMusic.Type];
            Managers.Instance.Game.AddCard(card);
        }
        GameStart().Forget();
    }

    private async UniTaskVoid GameStart()
    {
        await UniTask.Delay(3000);

        Managers.Instance.Game.GetComponentInScene<TextCanvas>().GameStart();

        PlayMusic(GetRandomMusic());
    }

    #region MusicGetAndSet

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

    private float _unitTime;
    public float UnitTime { get { return _unitTime; } }

    public MusicType GetRandomMusic()
    {
        List<Music> list = new List<Music>();

        GetPlayableMusicList(list);

        if(list.Count == 0)
        {
            _musicPlayer.time = 0f;
            ResetMusicCooldown();
            GetPlayableMusicList(list);
        }

        int rand = UnityEngine.Random.Range(0, list.Count);

        return list[rand].Type;
    }

    public void GetPlayableMusicList(List<Music> _musicList)
    {
        _musicList.Clear();

        foreach (Music music in _musicdata.MusicDictionary.Values)
        {
            if (PlayingMusic != music && music.Clip.length > _musicPlayer.time && music.PlayCoolDown <= 0f && Managers.Instance.Game.GetCardCount(Managers.Instance.Data.ConvertData.MusicType2MusicCardType[music.Type]) != 0)
            {
                _musicList.Add(music);
            }
        }
    }

    public async Awaitable ChangeMusic(MusicType type)
    {
        Debug.Log($"변경시작 : {type}");

        float time = MusicPlayTime;

        int beatPlayCount = 0;
        int corePlayCount = 0;
        int pianoPlayCount = 0;
        int drumPlayCount = 0;
        int guitarPlayCount = 0;
        int violinPlayCount = 0;
        int trumpetPlayCount = 0;
        int vocalPlayCount = 0;

        Music music = _musicdata.MusicDictionary[type];

        if(music.Clip.length < time)
        {
            time = 0f;
        }

        await Awaitable.BackgroundThreadAsync();

        while (_musicdata.BeatDictionary[type][beatPlayCount] < time)
        {
            beatPlayCount++;

            if (_musicdata.BeatDictionary[type].Count <= beatPlayCount)
            {
                break;
            }
        }

        CheckInstrumentsCount(type, time, _musicdata.CoreDictionary, ref corePlayCount);

        if (music.IsPianoUsable == true)
        {
            CheckInstrumentsCount(type, time, _musicdata.PianoDictionary, ref pianoPlayCount);
        }

        if (music.IsDrumUsable == true)
        {
            CheckInstrumentsCount(type, time, _musicdata.DrumDictionary, ref drumPlayCount);
        }

        if (music.IsGuitarUsable == true)
        {
            CheckInstrumentsCount(type, time, _musicdata.GuitarDictionary, ref guitarPlayCount);
        }

        if (music.IsViolinUsable == true)
        {
            CheckInstrumentsCount(type, time, _musicdata.ViolinDictionary, ref violinPlayCount);
        }

        if (music.IsTrumpetUsable == true)
        {
            CheckInstrumentsCount(type, time, _musicdata.TrumpetDictionary, ref trumpetPlayCount);
        }

        if (music.IsVocalUsable == true)
        {
            while (_musicdata.VocalDictionary[type][vocalPlayCount].StartTime <= time)
            {
                vocalPlayCount++;

                if (_musicdata.VocalDictionary[type].Count <= vocalPlayCount)
                {
                    break;
                }
            }
        }

        await Awaitable.MainThreadAsync();

        _beatPlayCount = beatPlayCount;
        _corePlayCount = corePlayCount;
        _pianoPlayCount = pianoPlayCount;
        _drumPlayCount = drumPlayCount;
        _guitarPlayCount = guitarPlayCount;
        _violinPlayCount = violinPlayCount;
        _trumpetPlayCount = trumpetPlayCount;
        _vocalPlayCount = vocalPlayCount;

        PlayMusic(type, time);
    }

    private void CheckInstrumentsCount(MusicType type, float time, Dictionary<MusicType, List<InstrumentsNote>> dict, ref int playCount)
    {
        while (dict[type][playCount].Timing < time)
        {
            playCount++;

            if (dict[type].Count <= playCount)
            {
                break;
            }
        }
    }

    private void PlayMusic(MusicType type, float time = 0f)
    {
        if (PlayingMusic != null)
        {
            PlayingMusic.PlayCoolDown = _musicPlayCooltime;
        }

        if(time == 0f)
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

    public void ResetMusicCooldown()
    {
        foreach(Music music in _musicdata.MusicDictionary.Values)
        {
            music.PlayCoolDown = 0f;
        }
    }

    #endregion

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
            ResetMusicCooldown();
            PlayMusic(GetRandomMusic());
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
            playCount++;

            if (dict[type].Count <= playCount)
            {
                break;
            }

            action?.Invoke(dict[type][playCount].IsHighNote);
        }
    }

    #endregion

    #region Setting

    public void SetMute(bool value)
    {
        _musicPlayer.mute = value;
    }

    #endregion
}
