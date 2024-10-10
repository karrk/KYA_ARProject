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

    private Animator _anim;
    private int _moveAnimHash;

    private void Start()
    {
        _cam = Camera.main.transform;
        Manager.Instance.Data.SetPlayer(this);
        _anim = GetComponent<Animator>();
        _moveAnimHash = Animator.StringToHash("MoveSpeed");
    }

    public void Move(Vector2 m_dir,float m_rate)
    {
        _camForward = _cam.forward;
        _camRight = _cam.right;

        _camForward.y = 0;
        _camRight.y = 0;

        _moveDir = (_camForward * m_dir.y + _camRight * m_dir.x).normalized;

        _rb.velocity = _moveDir * _moveSpeed * m_rate;

        if(_moveDir != Vector3.zero)
            transform.forward = _moveDir;

        _anim.SetFloat(_moveAnimHash, m_rate);
    }

    public void StopMove()
    {
        _anim.SetFloat(_moveAnimHash, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("DeadZone"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        Manager.Instance.Data.SetPlayer(null);
        Manager.Instance.UI.ShowMain();
        Manager.Instance.Data.SetPlayMode(false);

        Destroy(this.gameObject);
    }


}
