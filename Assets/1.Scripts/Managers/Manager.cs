using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance => _instance;

    [SerializeField] private PlayerController _player;
    public PlayerController Player => _player;

    public JoyStick JoyStick;

    private bool _playMode;
    public bool PlayMode => _playMode;

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

    public void SetPlayer(PlayerController m_player)
    {
        if(this._player != null)
        {
            Destroy(m_player.gameObject);
            Debug.LogError("플레이어가 이미 존재");
            return;
        }

        this._player = m_player;
        JoyStick.OnInputed += m_player.Move;
    }

    public void SetPlayMode(bool m_active)
    {
        this._playMode = m_active;
    }
}
