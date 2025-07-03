using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : BaseInit
{
    private Dictionary<Type, Component> _compoDictionary = new Dictionary<Type, Component>();

    protected override void Init()
    {
        base.Init();

        Component[] components = GetComponents<Component>(); //모든 컴포넌트 가져오기

        foreach (Component comp in components)
        {
            Type type = comp.GetType();
            if(!_compoDictionary.ContainsKey(type)) //Dictionary에 key가 중복되는지 확인하기
            {
                _compoDictionary.Add(type, comp); //Dictionary에 component 저장
            }
        }
    }

    public T GetT<T>() where T : Component
    {
        if(!_compoDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogError($"[사람이냐] : {typeof(T)} 컴포넌트가 존재하지 않습니다.");
            return null;
        }
        return _compoDictionary[typeof(T)] as T;
    }
}
