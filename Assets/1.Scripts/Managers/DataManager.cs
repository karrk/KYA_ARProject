using UnityEngine;

[System.Serializable]
public class DataManager
{
    [SerializeField] private Transform PlayerSpawnPos;

    public float GroundSize = 0.3f;

    public Vector3 GroundPos;

    public Vector3[] GroundPoese = new Vector3[(int)E_GroundPos.Size];

}
