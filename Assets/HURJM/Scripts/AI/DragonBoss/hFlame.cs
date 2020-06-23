using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelCollider(Collider other);

public class hFlame : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_ParticleSystem;
    private event DelCollider m_Contact;
    public event DelCollider contact { add { m_Contact += value; } remove { m_Contact -= value; } }

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

    private void OnTriggerEnter(Collider other) => m_Contact?.Invoke(other);
}
