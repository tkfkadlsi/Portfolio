using UnityEngine;

public class OnOffEffect : Effect
{
    public void SettingPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void OnEffect()
    {
        _particleSystem.Play();
    }

    public void OffEffect()
    {
        _particleSystem.Stop();
    }
}
