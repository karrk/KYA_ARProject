using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance => _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        { Destroy(this.gameObject); }
    }

    [SerializeField] private DataManager _data = null;
    public DataManager Data
    {
        get
        {
            if (_data == null)
                _data = new DataManager();

            return _data;
        }
    }
}
