using TMPro;
using UnityEngine;

public class SongInfoPanel : BaseUI
{
    enum ETexts
    {
        TypeText,
        BPMText,
        ArtistText,
        PianoText,
        DrumText,
        GuitarText,
        ViolinText,
        TrumpetText,
        VocalText
    }

    private TextMeshProUGUI _typeText;
    private TextMeshProUGUI _bpmText;
    private TextMeshProUGUI _artistText;
    private TextMeshProUGUI _pianoText;
    private TextMeshProUGUI _drumText;
    private TextMeshProUGUI _guitarText;
    private TextMeshProUGUI _violinText;
    private TextMeshProUGUI _trumpetText;
    private TextMeshProUGUI _vocalText;

    private readonly string _bpm = "BPM : ";
    private readonly string _krArtist = "작곡 : ";
    private readonly string _krPiano = "피아노 : ";
    private readonly string _krDrum = "드럼 : ";
    private readonly string _krGuitar = "기타 : ";
    private readonly string _krViolin = "바이올린 : ";
    private readonly string _krTrumpet = "트럼펫 : ";
    private readonly string _krVocal = "마이크 : ";

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));

        _typeText = Get<TextMeshProUGUI>((int)ETexts.TypeText);
        _bpmText = Get<TextMeshProUGUI>((int)ETexts.BPMText);
        _artistText = Get<TextMeshProUGUI>((int)ETexts.ArtistText);
        _pianoText = Get<TextMeshProUGUI>((int)ETexts.PianoText);
        _drumText = Get<TextMeshProUGUI>((int)ETexts.DrumText);
        _guitarText = Get<TextMeshProUGUI>((int)ETexts.GuitarText);
        _violinText = Get<TextMeshProUGUI>((int)ETexts.ViolinText);
        _trumpetText = Get<TextMeshProUGUI>((int)ETexts.TrumpetText);
        _vocalText = Get<TextMeshProUGUI>((int)ETexts.VocalText);
    }

    public void SetMusic(Music music)
    {
        _typeText.text = music.Name;
        _bpmText.text = _bpm + BpmCheck(music);
        _artistText.text = _krArtist + music.Artist;

        _pianoText.text = _krPiano + OXText(music.IsPianoUsable);
        _drumText.text = _krDrum + OXText(music.IsDrumUsable);
        _guitarText.text = _krGuitar + OXText(music.IsGuitarUsable);
        _violinText.text = _krViolin + OXText(music.IsViolinUsable);
        _trumpetText.text = _krTrumpet + OXText(music.IsTrumpetUsable);
        _vocalText.text = _krVocal + OXText(music.IsVocalUsable);
    }

    private string BpmCheck(Music music)
    {
        float maxBpm = float.MinValue;
        float minBpm = float.MaxValue;

        foreach(float bpm in music.BpmTimingDictionary.Values)
        {
            if(maxBpm < bpm)
            {
                maxBpm = bpm;
            }

            if(minBpm > bpm)
            {
                minBpm = bpm;
            }
        }

        if(minBpm == maxBpm)
        {
            return $"{minBpm}";
        }
        else
        {
            return $"{minBpm} ~ {maxBpm}";
        }
    }

    private string OXText(bool value)
    {
        return value == true ? "O" : "X";
    }
}
