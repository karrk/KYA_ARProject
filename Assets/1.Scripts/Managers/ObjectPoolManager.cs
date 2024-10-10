using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolManager
{
    private Dictionary<E_PoolType, ObjectPool> _poolTable = new Dictionary<E_PoolType, ObjectPool>();
    [SerializeField] private SerializableTable<E_PoolType, GameObject> _prefabs;

    public void Init()
    {
        foreach (var e in _prefabs)
        {
            _poolTable.Add(e.Key, new ObjectPool(e.Value,e.Key));
        }
    }

    public GameObject GetObject(E_PoolType m_type)
    {
        return _poolTable[m_type].GetObject();
    }

    public void ReturnObj(E_PoolType m_type,GameObject m_obj)
    {
        _poolTable[m_type].ReturnObj(m_obj);
    }
}

