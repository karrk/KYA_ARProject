using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarningController : MonoBehaviour
{
    [SerializeField] private RectTransform _rt;
    [SerializeField] private RectTransform _fillRt;

    [SerializeField] private Image _filler;
    [SerializeField] private Image _icon;

    private Vector3 _tempForward;
    private float _timer;
    private float _arriveTime;
    private float _rate;

    private bool _isStoped;
    private ReturnObject _ret;

    private static Vector3 StandardDirection; // ground forward

    private const float CellSpace = 0.32f;

    private RaycastHit[] _hitInfo;

    public Vector2 BoxSize => _boxSize;
    private Vector3 _boxSize = new Vector3(0.075f, 0.001f,0.075f);

    [SerializeField] private LayerMask _targetLayer;

    private void Awake()
    {
        _fillRt = _filler.GetComponent<RectTransform>();
    }

    public static void SetStandardDir(Vector3 m_dir)
    {
        StandardDirection = m_dir;
        StandardDirection.y = 0;
    }

    public void SetPos(E_GroundPos m_pos)
    {
        int idx = (int)m_pos;
        Vector3 pos = new Vector3();

        pos.x = (idx % 3 - 1) * CellSpace;
        pos.y = (1 - idx / 3) * CellSpace * -1;
        pos.z = 0f;

        _rt.localPosition = pos;
        _rt.localScale = Vector3.one;
    }
    public RaycastHit[] GetHitInfos()
    {
        return Physics.BoxCastAll(this.transform.position, _boxSize/2, Vector3.back, Quaternion.identity, _targetLayer);
    }

    public void SetArriveTime(float m_float)
    {
        _timer = 0;
        this._arriveTime = m_float;
        _fillRt.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        transform.up = StandardDirection;
        transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        _isStoped = false;
    }

    private void Update()
    {
        if (_isStoped)
            return;

        _timer += Time.deltaTime;
        _rate = _timer / _arriveTime - 0.05f;

        _fillRt.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _rate);

        IconCamFollow();
    }

    public void Stop()
    {
        if (_ret == null)
            _ret = GetComponent<ReturnObject>();

        _isStoped = true;
        Manager.Instance.Pool.ReturnObj(_ret.MyType, this.gameObject);
    }

    private void IconCamFollow()
    {
        _icon.transform.LookAt(Camera.main.transform.position);

        _icon.transform.eulerAngles = new Vector3(
            -90f, _icon.transform.eulerAngles.y, _icon.transform.eulerAngles.z);
    }
}
