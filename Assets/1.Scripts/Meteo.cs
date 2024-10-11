using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    private Transform _modelTr;
    private Vector3 _rot;
    private float _rotateAngle;
    private const float MinRotSpeed = 10f;
    private const float MaxRotSpeed = 30f;
    private const float MinMoveSpeed = 3f;
    private const float MaxMoveSpeed = 6f;

    private Coroutine _moveRoutine;

    private float _arriveTime;
    public float ArriveTime => _arriveTime;

    private Vector3 _dir;
    private float _moveSpeed;
    private Vector3 _targetPos;

    private ReturnObject _ret;
    private WarningController _myWarning;

    private SphereCollider _collider;
    private GameObject _subFxObject;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _modelTr = transform.GetChild(0);
        _subFxObject = transform.GetChild(1).gameObject;
        transform.localScale *= DataManager.ObjectScaleRate;
    }

    private void OnEnable()
    {
        _rot = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        _rotateAngle = Random.Range(MinRotSpeed, MaxRotSpeed);
        _moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
    }

    private void Start()
    {
        _ret = transform.GetComponent<ReturnObject>();
    }

    public void SetDestination(E_GroundPos m_pos)
    {
        _targetPos = Manager.Instance.Data.GroundPoese[(int)m_pos];
        _arriveTime = CalculateArriveTime(_targetPos);
        _myWarning = Manager.Instance.Pool.GetObject(E_PoolType.UI_Warning,
            Manager.Instance.Data.WarningCanvas).GetComponent<WarningController>();
        _myWarning.SetPos(m_pos);
        _myWarning.SetArriveTime(_arriveTime);

        transform.LookAt(_targetPos);
    }

    public void Shoot()
    {
        _moveRoutine = StartCoroutine(Move());
        _subFxObject.SetActive(true);
    }

    private float CalculateArriveTime(Vector3 m_destination)
    {
        float dist = Vector3.Distance(transform.position, m_destination);
        dist -= _collider.radius;

        return dist / _moveSpeed;
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position 
                = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);

            Rotate();

            yield return null;

        }
    }

    private void Rotate()
    {
        if (_rot.x != 0)
        {
            _rot.x += _rotateAngle * Time.deltaTime;
        }
        if (_rot.y != 0)
        {
            _rot.y += _rotateAngle * Time.deltaTime;
        }
        if (_rot.z != 0)
        {
            _rot.z += _rotateAngle * Time.deltaTime;
        }

        _modelTr.eulerAngles = _rot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ¾îµðµç Á¢ÃËÇÏ¸é Æø¹ß
        // ÀÌÆåÆ® ¹ß»ý

        if(collision.collider.CompareTag("Player"))
        {
            // Á×À½.
        }

        _myWarning.Stop();

        Manager.Instance.VFX.PlayFX(E_PoolType.VFX_exp0, this.transform.position);

        StopCoroutine(_moveRoutine);
        _subFxObject.SetActive(false);

        Manager.Instance.Pool.ReturnObj(_ret.MyType, this.gameObject);
    }


}
