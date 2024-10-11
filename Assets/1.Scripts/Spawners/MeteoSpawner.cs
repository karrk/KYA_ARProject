using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _leftBottomPoint;
    private Vector3 _rightTopPoint;

    [SerializeField] List<Meteo> _prefabs;

    private void Start()
    {
        GetRanges();
    }

    private void GetRanges()
    {
        int scaleStandard = 10 ;
        Vector3 scale = transform.lossyScale;
        _leftBottomPoint = new Vector3(-1 * scaleStandard * scale.x / 2, 0, -1 * scaleStandard * scale.z / 2);
        _rightTopPoint = new Vector3(_leftBottomPoint.x * -1, 0, _leftBottomPoint.z * -1);
    }

    private Vector3 GetRandomSpawnPos()
    {
        return new Vector3(
            Random.Range(_leftBottomPoint.x, _leftBottomPoint.x) + transform.position.x,
            transform.position.y,
            Random.Range(_leftBottomPoint.z, _rightTopPoint.z) + transform.position.z);
    }

    public void Spawn()
    {
        Vector3 spawnPos = GetRandomSpawnPos();

        E_PoolType rand = (E_PoolType)Random.Range((int)E_PoolType.Rock0, (int)E_PoolType.Rock_Size);

        Meteo meteo = Manager.Instance.Pool.GetObject(rand).GetComponent<Meteo>();
        meteo.transform.position = GetRandomSpawnPos();
        meteo.SetDestination((E_GroundPos)Random.Range(0, (int)E_GroundPos.Size));
        meteo.Shoot();
    }
}
