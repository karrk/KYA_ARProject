using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VFXManager
{
    [SerializeField] private List<ParticleSystem> _expFXs;

    public void PlayFX(Vector3 m_pos)
    {
        ParticleSystem particle = ParticleSystem.Instantiate(_expFXs[Random.Range(0, _expFXs.Count)]);
        particle.transform.position = m_pos;
    }
}
