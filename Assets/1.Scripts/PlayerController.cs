using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rb;
    private Transform _cam;

    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _moveDir;

    private void Start()
    {
        _cam = Camera.main.transform;
        Manager.Instance.Data.SetPlayer(this);
    }

    public void Move(Vector2 m_dir,float m_rate)
    {
        _camForward = _cam.forward;
        _camRight = _cam.right;

        _camForward.y = 0;
        _camRight.y = 0;

        _moveDir = (_camForward * m_dir.y + _camRight * m_dir.x).normalized;

        _rb.velocity = _moveDir * _moveSpeed * m_rate;
    }


}
