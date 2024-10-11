using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningController : MonoBehaviour
{
    [SerializeField] private RectTransform _rt;

    [SerializeField] private Image _filler;
    [SerializeField] private Image _icon;

    private Vector3 _tempForward;
    private float _timer;
    private float _arriveTime;
    private float _rate;

    private bool _isStoped;
    private ReturnObject _ret;

    private static Vector3 StandardDirection;

    private const float CellSpace = 0.32f;

    public static void SetStandardDir(Vector3 m_dir)
    {
        StandardDirection = m_dir;
        StandardDirection.x = 0;
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

    public void SetArriveTime(float m_float)
    {
        this._arriveTime = m_float;
    }

    private void OnEnable()
    {
        transform.up = StandardDirection;
        _isStoped = false;
        _timer = 0;

    }

    private void Update()
    {
        if (_isStoped)
            return;

        _timer += Time.deltaTime;
        _rate = _timer / _arriveTime;

        _filler.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _rate);

        _tempForward = Camera.main.transform.forward.z * Vector3.forward;
        _icon.transform.up = _tempForward;
    }

    public void Stop()
    {
        if (_ret == null)
            _ret = GetComponent<ReturnObject>();

        _isStoped = true;
        Manager.Instance.Pool.ReturnObj(_ret.MyType, this.gameObject);
    }


}
