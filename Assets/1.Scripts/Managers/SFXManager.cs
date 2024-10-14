using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SFXManager
{
    [SerializeField] private List<AudioClip> _fixedAudioClips;
    [SerializeField] private List<AudioClip> _moveableAudioClips;
    [SerializeField] private List<AudioClip> _bgmAudioClips;
    
    [SerializeField] private AudioSource _fixedSource;
    [SerializeField] private AudioSource _bgmSource;

    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private float _bgmLength;
    private float _addVolume = 0.05f;

    public void Init()
    {
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._GameStartBtn, VolumeUp);

        _bgmSource.volume = _minVolume;
        _bgmSource.clip = GetRandomClip(_bgmAudioClips);
        _bgmLength = GetEndTime(_bgmSource.clip);
        _bgmSource.Play();

        Manager.Instance.StartCoroutine(AutoBGMChange());
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

    private float GetEndTime(AudioClip m_clip)
    {
        return m_clip.length;
    }

    private AudioClip GetRandomClip(List<AudioClip> m_clips)
    {
        int rand = Random.Range(0, m_clips.Count);
        return m_clips[rand];
    }

    private void VolumeUp()
    {
        if (Manager.Instance.BGMRoutine != null)
            Manager.Instance.StopCoroutine(Manager.Instance.BGMRoutine);

        Manager.Instance.BGMRoutine = 
        Manager.Instance.StartCoroutine(VolumeToMax(_addVolume));
    }

    public void VolumeDown()
    {
        if (Manager.Instance.BGMRoutine != null)
            Manager.Instance.StopCoroutine(Manager.Instance.BGMRoutine);

        Manager.Instance.BGMRoutine =
        Manager.Instance.StartCoroutine(VolumeToMin(_addVolume));
    }

    private IEnumerator VolumeToMax(float m_value)
    {
        while (true)
        {
            if(_bgmSource.volume >= _maxVolume)
            {
                _bgmSource.volume = _maxVolume;
                break;
            }

            _bgmSource.volume += m_value;

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator VolumeToMin(float m_value)
    {
        while (true)
        {
            if(_bgmSource.volume <= _minVolume)
            {
                _bgmSource.volume = _minVolume;
                break;
            }

            _bgmSource.volume -= m_value;

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator AutoBGMChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(_bgmLength);

            AudioClip clip = GetRandomClip(_bgmAudioClips);
            _bgmSource.clip = clip;
            _bgmLength = GetEndTime(clip);
            _bgmSource.Play();
        }
    }
}