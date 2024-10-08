using UnityEngine;

[System.Serializable]
public class DataManager
{
    [SerializeField] private Transform PlayerSpawnPos;

    public Vector3 GroundSize = new Vector3(0.5f,0.5f,0.5f);

    public Vector3 GroundPos;

    public Vector3[] GroundPoese = new Vector3[(int)E_GroundPos.Size];

}
