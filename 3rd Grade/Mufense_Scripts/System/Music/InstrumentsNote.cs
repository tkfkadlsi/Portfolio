public class InstrumentsNote
{
    public InstrumentsNote(bool isHighNote, float timing)
    {
        IsHighNote = isHighNote;
        Timing = timing;
    }

    public bool IsHighNote { get; private set; }
    public float Timing { get; private set; }
}
