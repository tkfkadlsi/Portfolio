public class VocalNote
{
    public VocalNote(float start, float end)
    {
        StartTime = start;
        EndTime = end;
    }

    public float StartTime { get; private set; }
    public float EndTime { get; private set; }
}
