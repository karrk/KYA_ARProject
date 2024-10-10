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
    private const float MinMoveSpeed = 1f;
    private const float MaxMoveSpeed = 2f;

    private Coroutine _moveRoutine;

    private float _arriveTime;
    public float ArriveTime => _arriveTime;

    private Vector3 _dir;
    private float _moveSpeed;
    private Vector3 _targetPos;

    private ReturnObject _ret;

    private void Awake()
    {
        _ret = transform.AddComponent<ReturnObject>();
        _modelTr = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _rot = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        _rotateAngle = Random.Range(MinRotSpeed, MaxRotSpeed);
        _moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);

        _moveRoutine = StartCoroutine(Move());
    }

    public void SetDestination(E_GroundPos m_pos) // 스포너지정
    {
        _targetPos = Manager.Instance.Data.GroundPoese[(int)m_pos];
        _arriveTime = CalculateArriveTime(_targetPos);

        transform.LookAt(_targetPos);
    }

    private float CalculateArriveTime(Vector3 m_destination)
    {
        float dist = Vector3.Distance(transform.position, m_destination);

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
        // 어디든 접촉하면 폭발
        // 이펙트 발생

        if(collision.collider.CompareTag("Player"))
        {
            // 죽음.
        }

        Manager.Instance.VFX.PlayFX(E_PoolType.VFX_exp0, this.transform.position);

        StopCoroutine(_moveRoutine);

        Manager.Instance.Pool.ReturnObj(_ret.MyType, this.gameObject);
    }


}
