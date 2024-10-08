using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ground;

    private void Start()
    {
        InitGround();
    }

    private void InitGround()
    {
        _ground.transform.localScale = Manager.Instance.Data.GroundSize;
    }
}
