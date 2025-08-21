using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoiseController : MonoBehaviour
{
    private readonly float _cooltime = 2f;
    private float _cooldown = 0f;

    private int _noiseLevel;

    private void OnEnable()
    {
        Managers.Instance.Game.CoreHPChangeEvent += CoreHPChangeHandler;
    }

    private void OnDisable()
    {
        Managers.Instance.Game.CoreHPChangeEvent -= CoreHPChangeHandler;
    }

    private void CoreHPChangeHandler(float hp)
    {
        if (hp < 15f)
        {
            _noiseLevel = 4;
        }
        else if(hp < 30f)
        {
            _noiseLevel = 3;
        }
        else if(hp < 50f)
        {
            _noiseLevel = 2;
        }
        else if(hp < 75)
        {
            _noiseLevel = 1;
        }
        else
        {
            _noiseLevel = 0;
        }
    }

    private void Update()
    {
        _cooldown += Time.deltaTime;

        if(_cooltime < _cooldown)
        {
            _cooldown -= _cooltime;

            if(IsExecuteNoise())
            {
                Noise(SetNoiseTime()).Forget();
            }
        }
    }

    private bool IsExecuteNoise()
    {
        float percent = _noiseLevel * 0.2f;

        return Random.value < percent;
    }

    private float SetNoiseTime()
    {
        float noiseTime = Mathf.Clamp(Random.value * _noiseLevel * 0.2f, 0.25f, 1.5f);

        return noiseTime;
    }

    private async UniTask Noise(float noiseTime)
    {
        NoiseOn();
        await UniTask.Delay((int)(noiseTime * 1000));
        NoiseOff();
    }

    private void NoiseOn()
    {
        Managers.Instance.Game.GetComponentInScene<MusicPlayer>().SetMute(true);
        Managers.Instance.Game.GetComponentInScene<SoundNoise>().NoisePlay();
        Managers.Instance.Game.GetComponentInScene<CameraNoise>().NoiseOn();
    }

    private void NoiseOff()
    {
        Managers.Instance.Game.GetComponentInScene<MusicPlayer>().SetMute(false);
        Managers.Instance.Game.GetComponentInScene<SoundNoise>().NoiseStop();
        Managers.Instance.Game.GetComponentInScene<CameraNoise>().NoiseOff();
    }
}
