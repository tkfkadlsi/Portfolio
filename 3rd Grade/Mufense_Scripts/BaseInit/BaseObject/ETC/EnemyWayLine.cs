using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolableObject))]
public class EnemyWayLine : BaseObject
{
    private readonly float _spawnCooltime = 1.5f;
    private float _spawnCooldown;

    private List<EnemyWayArrow> _arrowList = new List<EnemyWayArrow>();

    private void Update()
    {
        _spawnCooldown += Time.deltaTime;

        if(_spawnCooldown > _spawnCooltime)
        {
            _spawnCooldown -= _spawnCooltime;
            CreateWayArrow();
        }
    }

    private void CreateWayArrow()
    {
        EnemyWayArrow arrow = Managers.Instance.Pool.PopObject(PoolType.EnemyWayArrow, transform.position).GetComponent<EnemyWayArrow>();
        Vector3[] positions = new Vector3[2];

        if(IsGreaterX(transform.position))
        {
            positions[0] = new Vector3(transform.position.x, 0.1f, 0);
        }
        else
        {
            positions[0] = new Vector3(0, 0.1f, transform.position.z);
        }

        positions[1] = new Vector3(0, 0.1f, 0);

        arrow.SettingPositions(positions);
        _arrowList.Add(arrow);
    }

    public void SettingLine()
    {
        LineRenderer line = GetT<LineRenderer>();

        line.SetPosition(0, new Vector3(transform.position.x, 0.1f, transform.position.z));
        line.SetPosition(2, new Vector3(0, 0.1f, 0));

        if (IsGreaterX(transform.position))
        {
            line.SetPosition(1, new Vector3(transform.position.x, 0.1f, 0));
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0.1f, transform.position.z));
        }

        _spawnCooldown = 0;
    }

    private bool IsGreaterX(Vector3 position)
    {
        return Mathf.Abs(position.x) > Mathf.Abs(position.z);
    }

    public void PushThisObject()
    {
        foreach(EnemyWayArrow arrow in _arrowList)
        {
            arrow.PushThisObject();
        }

        GetT<PoolableObject>().PushThisObject();
    }
}
