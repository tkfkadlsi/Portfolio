using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class BaseUI : BaseInit
{
    private Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();

    private void OnEnable()
    {
        ActiveOn();
    }

    private void OnDisable()
    {
        ActiveOff();
    }

    protected virtual void ActiveOn()
    {
        Debug.Log($"[����̳�] : {name} active is on");
    }

    protected virtual void ActiveOff()
    {
        Debug.Log($"[����̳�] : {name} active is off");
    }

    protected void Bind<T>(Type type) where T : Object
    {
        string[] names = Enum.GetNames(type);
        Object[] objects = new Object[names.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Utility.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Utility.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.LogError($"[����̳�] : {names[i]} (��)��� ������Ʈ�� �����ϴ�.");
            }
        }
    }

    protected T Get<T>(int idx) where T : Object
    {
        if (_objects.TryGetValue(typeof(T), out Object[] objects))
        {
            return objects[idx] as T;
        }


        Debug.LogError($"[����̳�] : {typeof(T)} Ÿ���� Bind���� �ʾҽ��ϴ�.");
        return null;
    }
}
