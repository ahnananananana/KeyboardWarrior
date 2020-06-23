using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hFlame : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    private void Awake()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public void OnSet(bool inSet)
    {
        if (inSet)
            m_ParticleSystem.Play();
        else
            m_ParticleSystem.Stop();
    }
}
