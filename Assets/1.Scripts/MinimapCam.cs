using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    private void Update()
    {
        if (!Manager.Instance.Data.PlayMode || Manager.Instance.Data.Player == null)
            return;

        transform.LookAt(Manager.Instance.Data.Player.transform);
    }
}
