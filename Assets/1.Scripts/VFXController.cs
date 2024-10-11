using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    private ParticleSystem _fx;
    private ReturnObject _ret;

    private void Start()
    {
        _ret = GetComponent<ReturnObject>();
        _fx = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _fx.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        transform.localScale *= DataManager.ObjectScaleRate;
    }

    private void OnParticleSystemStopped()
    {
        Manager.Instance.Pool.ReturnObj(_ret.MyType, this.gameObject);
    }
}
