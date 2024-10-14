using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SFXManager
{
    [SerializeField] private List<AudioClip> _fixedAudioClips;
    [SerializeField] private List<AudioClip> _moveableAudioClips;
    [SerializeField] private AudioSource _fixedSource;

    //public void Init()
    //{
    //    GameObject source = new GameObject();
    //    source.name = "FixedSFX";
    //    source.transform.SetParent(Manager.Instance.transform);
    //    _fixedSource = source.AddComponent<AudioSource>();
    //    SetFixedSourceOptions();
    //}

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

    //private void SetFixedSourceOptions()
    //{
    //    _fixedSource.playOnAwake = false;
    //    _fixedSource.pitch = 0.8f;
    //    _fixedSource.spatialBlend = 1f;
    //    _fixedSource.rolloffMode = AudioRolloffMode.Linear;
    //    _fixedSource.minDistance = 0f;
    //    _fixedSource.maxDistance = 10f;
    //}
}