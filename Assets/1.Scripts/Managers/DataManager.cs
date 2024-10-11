using UnityEngine;

[System.Serializable]
public class DataManager
{
    [SerializeField] private Transform PlayerSpawnPos;

    public const float ObjectScaleRate = 0.01f;

    public Vector3 GroundPos;

    public Vector3[] GroundPoese = new Vector3[(int)E_GroundPos.Size];

    public Vector3 GroundViewpoint = new Vector3();

    [SerializeField] private PlayerController _player;
    public PlayerController Player => _player;

    [SerializeField] private Transform _warningCanvas;
    public Transform WarningCanvas => _warningCanvas;

    private bool _playMode;
    public bool PlayMode => _playMode;

    public void SetPlayer(PlayerController m_player)
    {
        if (this._player != null && m_player != null)
        {
            GameObject.Destroy(m_player.gameObject);
            Debug.LogError("플레이어가 이미 존재");
            return;
        }

        this._player = m_player;
    }

    public void SetPlayMode(bool m_active)
    {
        this._playMode = m_active;
    }
}
