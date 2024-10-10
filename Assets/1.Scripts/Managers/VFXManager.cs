using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager
{
    public void PlayFX(E_PoolType m_fxType, Vector3 m_pos)
    {
        if (E_PoolType.VFX_exp0 > m_fxType || m_fxType >= E_PoolType.VFX_Exp_Size)
            throw new System.Exception("¿Ã∆Â∆Æ ø¿∫Í¡ß∆Æ∞° æ∆¥‘");

        ParticleSystem particle = Manager.Instance.Pool.GetObject(m_fxType).GetComponent<ParticleSystem>();
        particle.transform.position = m_pos;
        particle.Play();
    }


}
