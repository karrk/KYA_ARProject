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

        UI.Init();
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

    [SerializeField] private UIManager _ui = null;
    public UIManager UI
    {
        get
        {
            if (_ui == null)
                _ui = new UIManager();

            return _ui;
        }
    }

    [SerializeField] private VFXManager _vfx = null;
    public VFXManager VFX
    {
        get
        {
            if (_vfx == null)
                _vfx = new VFXManager();

            return _vfx;
        }
    }

    public void EndGame()
    {

    }
}
