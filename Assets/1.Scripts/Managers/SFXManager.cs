using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SFXManager
{
    [SerializeField] private List<AudioClip> _fixedAudioClips;
    [SerializeField] private List<AudioClip> _moveableAudioClips;
    private AudioSource _fixedSource;

    public void Init()
    {
        GameObject source = new GameObject();
        source.name = "FixedSFX";
        source.transform.SetParent(Manager.Instance.transform);

        Manager.Instance.UI.AddBtnEvnet(
            Manager.Instance.UI._GameStartBtn, SetCenterPos);
    }

    public void PlayFX(E_SoundType m_fxType)
    {
        _fixedSource.PlayOneShot(_fixedAudioClips[(int)m_fxType]);
    }

    public void PlayFX(E_SoundType m_fxType, Transform m_requester)
    {
        AudioSource source = m_requester.GetComponent<AudioSource>() != null ?
            m_requester.GetComponent<AudioSource>() :
            m_requester.AddComponent<AudioSource>();

        source.PlayOneShot(_moveableAudioClips[(int)m_fxType]);
    }

    public AudioClip GetAudioClip(E_SoundType m_fxType)
    {
        return _moveableAudioClips[(int)m_fxType];
    }

    private void SetCenterPos()
    {
        _fixedSource.transform.position = Manager.Instance.Data.GroundPos;
    }
}