using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _completeStopDelay;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _targetcam;

    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _moveDir;

    private Animator _anim;
    private int _moveAnimHash;

    private Coroutine _stopRoutine;

    private void Start()
    {
        _targetcam = Camera.main.transform.GetChild(0).transform;
        Manager.Instance.Data.SetPlayer(this);
        _anim = GetComponent<Animator>();
        _moveAnimHash = Animator.StringToHash("MoveSpeed");
        transform.localScale *= DataManager.ObjectScaleRate;
        _moveSpeed *= DataManager.ObjectScaleRate;
    }

    public void Move(Vector2 m_dir,float m_rate)
    {
        if (_stopRoutine != null)
            StopCoroutine(_stopRoutine);

        _camForward = _targetcam.forward;
        _camRight = _targetcam.right;

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
        _stopRoutine = StartCoroutine(SlowStopRoutine());
    }

    private IEnumerator SlowStopRoutine()
    {
        float speed = _rb.velocity.magnitude;
        float timer = 0f;

        while (true)
        {
            if (speed == 0)
                break;

            timer += Time.deltaTime;
            speed = Mathf.Lerp(speed, 0, timer * _completeStopDelay);

            _rb.velocity = _moveDir * speed;
            _anim.SetFloat(_moveAnimHash, _rb.velocity.magnitude * 5);

            yield return null;
        }
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

        if (_stopRoutine != null)
            StopCoroutine(_stopRoutine);

        Destroy(this.gameObject);
    }


}
