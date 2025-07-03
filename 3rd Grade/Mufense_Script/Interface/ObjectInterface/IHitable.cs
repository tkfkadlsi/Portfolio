public interface IHitable
{
    public float MaxHP { get; }
    public float HP { get; }

    public void Hit(float damage, InstrumentsTower attacker = null);
}
