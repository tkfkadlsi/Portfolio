using UnityEngine;

public class SoundNoise : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void NoisePlay()
    {
        _audioSource.time = Random.value * 10f;
        _audioSource.Play();
    }

    public void NoiseStop()
    {
        _audioSource.Stop();
    }
}
