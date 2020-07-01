using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class hAudioManager
{
    private List<AudioSource> m_AudioSourcePool;
    private GameObject m_Player;
    private int m_PoolSize;

    [SerializeField]
    private AudioMixerGroup m_MixerGroup;
    [SerializeField]
    private float m_PitchWidth;

    public int poolSize { get => m_PoolSize; set => m_PoolSize = value; }

    public hAudioManager()
    {
        m_AudioSourcePool = new List<AudioSource>();
        m_PoolSize = 1;
    }

    public void Init(GameObject inPlayer)
    {
        m_Player = inPlayer;

        for (int i = 0; i < m_PoolSize; ++i)
            AddNewSource();
    }

    private AudioSource AddNewSource()
    {
        var newSource = m_Player.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        newSource.outputAudioMixerGroup = m_MixerGroup;
        newSource.enabled = false;
        m_AudioSourcePool.Add(newSource);
        return newSource;
    }

    public AudioSource PlayClip(AudioClip inClip, bool inIsLoop = false)
    {
        AudioSource source = null;

        for (int i = 0; i < m_AudioSourcePool.Count; ++i)
        {
            var temp = m_AudioSourcePool[i];
            if(!temp.isPlaying)
            {
                source = temp;
                break;
            }
        }

        if(source == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Not Enough Pool size!");
#endif
            source = AddNewSource();
            m_AudioSourcePool.Add(source);
        }

        source.enabled = true;
        float min = 1f - .01f * m_PitchWidth;
        float max = 1f + .01f * m_PitchWidth;
        source.pitch *= Random.Range(min, max);
        source.clip = inClip;
        source.loop = inIsLoop;
        source.Play();
        return source;
    }

    public void StopClip(AudioClip inClip)
    {
        for (int i = 0; i < m_AudioSourcePool.Count; ++i)
        {
            var source = m_AudioSourcePool[i];
            if(source.clip == inClip)
            {
                source.Stop();
                source.clip = null;
                source.enabled = false;
                break;
            }
        }
    }
}
