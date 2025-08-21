using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PoolableObject))]
public class EnemyWayArrow : BaseObject
{
    private Vector3[] _positions;
    private int _counter;
    private readonly float _speed = 3f;

    private readonly Vector3 _rotAxis = new Vector3(0, 0, 1);

    public void SettingPositions(Vector3[] positions)
    {
        _positions = positions;
        _counter = 0;

        if(_positions.Length > 0)
        {
            Movement().Forget();
        }
    }

    private async UniTask Movement()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(1, 0, 0), 90);

        Vector3 startPos = transform.position;
        Vector3 endPos = _positions[_counter];

        Vector3 direction = endPos - startPos;

        if (direction.z > 1f)
        {
            transform.Rotate(_rotAxis, 0f);
        }
        else if (direction.z < -1f)
        {
            transform.Rotate(_rotAxis, 180f);
        }
        else if(direction.x > 1f)
        {
            transform.Rotate(_rotAxis, -90f);
        }
        else if(direction.x < -1f)
        {
            transform.Rotate(_rotAxis, 90f);
        }

        float distance = Vector3.Distance(startPos, endPos);

        float lerpTime = distance / _speed;
        float t = 0f;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            await UniTask.Yield();

            if (gameObject.activeInHierarchy == false) return;

            transform.position = Vector3.Lerp(startPos, endPos, t / lerpTime);
        }

        _counter++;

        if(_counter == _positions.Length)
        {
            MoveFinish();
        }
        else
        {
            Movement().Forget();
        }
    }

    private void MoveFinish()
    {
        PushThisObject();
    }

    public void PushThisObject()
    {
        GetT<PoolableObject>().PushThisObject();
    }
}
