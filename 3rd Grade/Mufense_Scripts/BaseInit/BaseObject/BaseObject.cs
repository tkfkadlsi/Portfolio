using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : BaseInit
{
    private Dictionary<Type, Component> _compoDictionary = new Dictionary<Type, Component>();

    protected override void Init()
    {
        base.Init();

        Component[] components = GetComponents<Component>(); //��� ������Ʈ ��������

        foreach (Component comp in components)
        {
            Type type = comp.GetType();
            if(!_compoDictionary.ContainsKey(type)) //Dictionary�� key�� �ߺ��Ǵ��� Ȯ���ϱ�
            {
                _compoDictionary.Add(type, comp); //Dictionary�� component ����
            }
        }
    }

    public T GetT<T>() where T : Component
    {
        if(!_compoDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogError($"[����̳�] : {typeof(T)} ������Ʈ�� �������� �ʽ��ϴ�.");
            return null;
        }
        return _compoDictionary[typeof(T)] as T;
    }
}
