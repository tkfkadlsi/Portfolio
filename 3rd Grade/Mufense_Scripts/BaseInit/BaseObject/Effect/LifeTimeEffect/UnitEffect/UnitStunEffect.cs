public class UnitStunEffect : LifeTimeEffect
{
    private Unit _unit;

    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }

    private void Update()
    {
        transform.position = _unit.transform.position;
    }
}
