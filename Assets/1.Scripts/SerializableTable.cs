using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

[System.Serializable]
public class SerializableTable<T,U>
{
    [SerializeField] private List<T> _keys = new List<T>();
    [SerializeField] private List<U> _values = new List<U>();

    public U this[T key]
    {
        get 
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                if(key.Equals(_keys[i]))
                {
                    return _values[i];
                }
            }

            throw new System.Exception("해당 데이터를 찾을 수 없음");
        }
        set
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                if (key.Equals(_keys[i]))
                {
                    _values[i] = value;
                }
            }

            throw new System.Exception("할당 된 키가 없음");
        }
    }

    public int Count => _keys.Count;

    public void Add(T m_key,U m_value)
    {
        _keys.Add(m_key);
        _values.Add(m_value);
    }

    public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            yield return new KeyValuePair<T, U>(_keys[i], _values[i]);
        }
    }
}
