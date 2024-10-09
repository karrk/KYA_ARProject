using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(_playerPrefab);
        player.transform.position = transform.position;
        player.transform.SetParent(null);
    }
}
