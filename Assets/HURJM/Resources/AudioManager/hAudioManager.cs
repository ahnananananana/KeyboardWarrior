using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hAudioManager
{
    private List<AudioSource> m_AudioSourcePool;
    [SerializeField]
    private int m_PoolSize;
    [SerializeField]
    private GameObject m_Player;

    public hAudioManager(GameObject inPlayer, int inPoolSize = 3)
    {
        m_Player = inPlayer;
        m_PoolSize = inPoolSize;

        m_AudioSourcePool = new List<AudioSource>();

        for (int i = 0; i < m_PoolSize; ++i)
            AddNewSource();
    }

    private AudioSource AddNewSource()
    {
        var newSource = m_Player.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
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

        source.pitch *= Random.Range(.85f, 1.15f);
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
                break;
            }
        }
    }
}
