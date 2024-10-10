using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject _prefab;
    private int _objectCount = 5;

    private List<GameObject> _list = new List<GameObject>();
    private Transform _directory;

    private E_PoolType _type;

    public ObjectPool(GameObject m_prefab, E_PoolType m_type)
    {
        this._type = m_type;
        this._prefab = m_prefab;
        CreateMyDirectory();
        Init();
    }

    private void CreateMyDirectory()
    {
        GameObject newDir = new GameObject();
        newDir.name = $"{_prefab.name} Pool";
        newDir.transform.SetParent(Manager.Instance.transform);
        _directory = newDir.transform;
    }

    private void Init()
    {
        CreateObject(_objectCount);
    }

    private void CreateObject(int m_createCount)
    {
        bool hasReturn = _prefab.TryGetComponent<ReturnObject>(out ReturnObject temp);

        for (int i = 0; i < m_createCount; i++)
        {
            GameObject newObj = GameObject.Instantiate(_prefab);
            newObj.SetActive(false);
            newObj.transform.SetParent(_directory);

            if (hasReturn)
                newObj.GetComponent<ReturnObject>().MyType = this._type;
            else
                newObj.AddComponent<ReturnObject>().MyType = this._type;

            _list.Add(newObj);
        }
    }

    public GameObject GetObject()
    {
        if (_list.Count <= 0)
        {
            _objectCount *= 2;
            CreateObject(_objectCount);

            return GetObject();
        }

        else
        {
            GameObject obj = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            obj.SetActive(true);

            return obj;
        }
    }

    public void ReturnObj(GameObject m_obj)
    {
        m_obj.SetActive(false);
        _list.Add(m_obj);
    }
}
