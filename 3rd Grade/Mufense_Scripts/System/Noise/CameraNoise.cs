using UnityEngine;

public class CameraNoise : MonoBehaviour
{
    private bool _noiseOn;
    private Vector3 _noiseVector;
    private readonly Vector3 _originVector = new Vector3(0, 0, 0);

    private void Update()
    {
        if( _noiseOn )
        {
            _noiseVector.x = Random.Range(-1.0f, 1.0f);
            _noiseVector.y = Random.Range(-1.0f, 1.0f);
            _noiseVector.z = Random.Range(-1.0f, 1.0f);

            transform.localPosition = _originVector + _noiseVector;
        }
    }

    public void NoiseOn()
    {
        _noiseOn = true;
    }

    public void NoiseOff()
    {
        transform.localPosition = _originVector;
        _noiseOn = false;
    }
}
