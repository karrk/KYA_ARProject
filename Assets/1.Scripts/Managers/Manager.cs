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
        Pool.Init();
        SFX.Init();
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

    private VFXManager _vfx = null;
    public VFXManager VFX
    {
        get
        {
            if (_vfx == null)
                _vfx = new VFXManager();

            return _vfx;
        }
    }

    [SerializeField] private ObjectPoolManager _pool = null;
    public ObjectPoolManager Pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPoolManager();

            return _pool;
        }
    }

    [SerializeField] private SFXManager _sfx = null;
    public SFXManager SFX
    {
        get
        {
            if (_sfx == null)
                _sfx = new SFXManager();

            return _sfx;
        }
    }

    [SerializeField] private MeteoSpawner _meteoSpawner;

    private float _meteoSpawnDelay = 3f;
    private float _timer;
    public Coroutine BGMRoutine;

    private void Update()
    {
        if (!Data.PlayMode)
            return;

        _timer += Time.deltaTime;

        if(_timer >= _meteoSpawnDelay)
        {
            _meteoSpawner.Spawn();
            _timer = 0;
        }    
    }

}
