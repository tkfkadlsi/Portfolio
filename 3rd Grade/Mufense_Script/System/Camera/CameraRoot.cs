using DG.Tweening;
using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _targetPosition;

    public void SetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _targetPosition += transform.forward * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _targetPosition += -transform.forward * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _targetPosition += transform.right * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _targetPosition += -transform.right * _speed * Time.deltaTime;
        }

        _targetPosition.y = 0;

        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -20f, 20f);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, -20f, 20f);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
    }
}
