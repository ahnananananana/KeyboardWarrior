using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hParticleSystem : MonoBehaviour
{
    private event DelVoid m_EndEvent;
    public event DelVoid endEvent { add { m_EndEvent += value; } remove { m_EndEvent -= value; } }
    [SerializeField]
    private ParticleSystem m_ParticleSystem;
    private hParticleSystem[] m_Children;
    private bool m_IsEnd;
    public bool isEnd => m_IsEnd;
    // Start is called before the first frame update
    private void Awake()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        var main = m_ParticleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        ParticleSystem[] childrenParticles = GetComponentsInChildren<ParticleSystem>();
        m_Children = new hParticleSystem[childrenParticles.Length - 1];

        for (int i = 1; i < childrenParticles.Length; ++i)
        {
            var child = childrenParticles[i].gameObject.AddComponent<hParticleSystem>();
            child.endEvent += ChildEnd;
            m_Children[i - 1] = child;
        }
    }


    public void Play() => m_ParticleSystem.Play();
    public void Stop() => m_ParticleSystem.Stop();

    private void ChildEnd()
    {
        if(IsChildrenEnd() && m_ParticleSystem.isStopped)
        {
            m_IsEnd = true;
            m_EndEvent?.Invoke();
        }
    }

    private bool IsChildrenEnd()
    {
        for (int i = 0; i < m_Children.Length; ++i)
        {
            if (!m_Children[i].isEnd)
                return false;
        }
        return true;
    }

    private void OnParticleSystemStopped()
    {
        Debug.Log("Stop Particle");
        if (IsChildrenEnd())
        {
            m_IsEnd = true;
            m_EndEvent?.Invoke();
        }
    }
}
