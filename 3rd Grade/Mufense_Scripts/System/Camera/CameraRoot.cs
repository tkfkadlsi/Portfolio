using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    private Vector3 _targetPosition;

    public void SetPosition(Vector3 position)
    {
        position.y = 10;

        Vector3 direction = transform.forward;
        direction.y = 0;
        direction = direction.normalized;

        _targetPosition = position + -direction * 3.5f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
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

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 1, 0), _speed * 10f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0), -_speed * 10f * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed /= 2;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel") * _speed;

        _targetPosition.y -= scroll;

        if (_targetPosition.y < _minY)
        {
            _targetPosition.y = _minY;
        }
        if (_targetPosition.y > _maxY)
        {
            _targetPosition.y = _maxY;
        }

        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -20f, 20f);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, -20f, 20f);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
    }
}
