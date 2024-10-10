using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{
    private Vector3 _leftBottomPoint;
    private Vector3 _rightTopPoint;

    [SerializeField] List<Meteo> _prefabs;

    private void Start()
    {
        GetRanges();
    }

    private void GetRanges()
    {
        int scaleStandard = 10;
        Vector3 scale = transform.localScale;
        _leftBottomPoint = new Vector3(-1 * scaleStandard * scale.x / 2, 0, -1 * scaleStandard * scale.z / 2);
        _rightTopPoint = new Vector3(_leftBottomPoint.x * -1, 0, _leftBottomPoint.z * -1);
    }

    private Vector3 GetRandomSpawnPos()
    {
        return new Vector3(
            Random.Range(_leftBottomPoint.x, _rightTopPoint.x) + transform.position.x,
            transform.position.y,
            Random.Range(_leftBottomPoint.z, _rightTopPoint.z) + transform.position.z);
    }

    public void Spawn()
    {
        Vector3 spawnPos = GetRandomSpawnPos();

        Meteo meteo = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)]);
        meteo.SetDestination((E_GroundPos)Random.Range(0, (int)E_GroundPos.Size));

        meteo.transform.position = GetRandomSpawnPos();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Spawn();
    }


}
