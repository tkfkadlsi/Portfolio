public enum Debuffs
{
    None = 0,
    Stun = 1,
}

public interface IHealth
{
    public float HP { get; set; }
    public HPSlider HPSlider { get; set; }
    public void Hit(float damage, int debuff = 0, Tower attacker = null);
    public void Die();
}
