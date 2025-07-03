using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MusicSO")]
public class Music : ScriptableObject
{
    public MusicType Type;
    public string Artist;
    public AudioClip Clip;
    [SerializedDictionary("Timing", "Bpm")] public SerializedDictionary<float, float> BpmTimingDictionary = new SerializedDictionary<float, float>();
    public TextAsset CoreText;

    public TextAsset PianoText;
    public bool IsPianoUsable;

    public TextAsset DrumText;
    public bool IsDrumUsable;

    public TextAsset GuitarText;
    public bool IsGuitarUsable;

    public TextAsset ViolinText;
    public bool IsViolinUsable;

    public TextAsset TrumpetText;
    public bool IsTrumpetUsable;

    public TextAsset VocalText;
    public bool IsVocalUsable;

    public float PlayCoolDown;
}
