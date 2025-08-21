using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataStorage : MonoBehaviour
{
    [SerializeField] private List<Music> _musicList = new List<Music>();

    public Dictionary<MusicType, Music> MusicDictionary { get; private set; } = new Dictionary<MusicType, Music>();
    public Dictionary<MusicType, List<BpmNote>> BpmDictionary { get; private set; } = new Dictionary<MusicType, List<BpmNote>>();
    public Dictionary<MusicType, List<float>> BeatDictionary { get; private set; } = new Dictionary<MusicType, List<float>>();
    public Dictionary<MusicType, List<InstrumentsNote>> CoreDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<InstrumentsNote>> PianoDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<InstrumentsNote>> DrumDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<InstrumentsNote>> GuitarDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<InstrumentsNote>> ViolinDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<InstrumentsNote>> TrumpetDictionary { get; private set; } = new Dictionary<MusicType, List<InstrumentsNote>>();
    public Dictionary<MusicType, List<VocalNote>> VocalDictionary { get; private set; } = new Dictionary<MusicType, List<VocalNote>>();


    private void Awake()
    {
        foreach (Music music in _musicList)
        {
            AddMusic(music);
        }
    }

    public void AddMusic(Music music)
    {
        if (MusicDictionary.ContainsKey(music.Type)) return;

        music.PlayCoolDown = 0f;
        MusicDictionary.Add(music.Type, music);
        
        BeatDictionary.Add(music.Type, new List<float>());
        MakeBpmInfo(music);
        MakeBeatInfo(music);
                 
        CoreDictionary.Add(music.Type, NoteParse(music.CoreText.text));
        PianoDictionary.Add(music.Type, NoteParse(music.PianoText.text));
        DrumDictionary.Add(music.Type, NoteParse(music.DrumText.text));
        GuitarDictionary.Add(music.Type, NoteParse(music.GuitarText.text));
        ViolinDictionary.Add(music.Type, NoteParse(music.ViolinText.text));
        TrumpetDictionary.Add(music.Type, NoteParse(music.TrumpetText.text));    
        VocalDictionary.Add(music.Type, VocalParse(music.VocalText.text));

    }

    private void MakeBpmInfo(Music music)
    {
        List<BpmNote> bpmNotes = new List<BpmNote>();

        foreach(var kv in music.BpmTimingDictionary)
        {
            BpmNote bpmNote = new BpmNote(kv.Key, kv.Value);
            bpmNotes.Add(bpmNote);
        }

        BpmDictionary.Add(music.Type, bpmNotes);
    }

    private void MakeBeatInfo(Music music)
    {
        List<float> timings = new List<float>();

        foreach (var kv in music.BpmTimingDictionary)
        {
            timings.Add(kv.Key);
        }

        float timing = 0f;
        float unitTime = 0f;

        for (int i = 0; i < timings.Count; i++)
        {
            timing = timings[i];
            unitTime = 60f / music.BpmTimingDictionary[timings[i]];

            while (timing < (i == timings.Count - 1 ? music.Clip.length : timings[i + 1]))
            {
                BeatDictionary[music.Type].Add(timing);
                timing += unitTime;
            }
        }
    }

    private List<InstrumentsNote> NoteParse(string text)
    {
        List<InstrumentsNote> noteList = new List<InstrumentsNote>();

        if(string.IsNullOrEmpty(text))
        {
            return noteList;
        }

        string[] notes = text.Split('\n');

        foreach (string note in notes)
        {
            string[] infos = note.Split(',');
            bool ishigh = IsHighParse(infos[0]);
            float timing = int.Parse(infos[2]);

            timing *= 0.001f;

            noteList.Add(new InstrumentsNote(ishigh, timing));
        }

        return noteList;
    }

    private List<VocalNote> VocalParse(string text)
    {
        List<VocalNote> noteList = new List<VocalNote>();

        if(string.IsNullOrEmpty(text))
        {
            return noteList;
        }

        string[] notes = text.Split('\n');

        foreach (string note in notes)
        {
            string[] infos = note.Split(',');
            string[] endinfos = infos[5].Split(':');

            float start = int.Parse(infos[2]);
            float end = int.Parse(endinfos[0]);

            noteList.Add(new VocalNote(start * 0.001f, end * 0.001f));
        }

        return noteList;
    }


    private readonly string lowstr = "128";
    private bool IsHighParse(string str)
    {
        if (str == lowstr)
            return false;
        else
            return true;
    }
}
