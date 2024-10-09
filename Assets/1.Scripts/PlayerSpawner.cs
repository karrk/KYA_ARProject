using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._GameStartBtn, SpawnPlayer);
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(_playerPrefab);
        player.transform.position = transform.position;
        player.transform.SetParent(null);
    }
}
