using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelCollider(Collider other);

public class hFlame : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_ParticleSystem;
    [SerializeField]
    private Collider[] m_Collider;
    private event DelCollider m_Contact;
    public event DelCollider contact { add { m_Contact += value; } remove { m_Contact -= value; } }

    private void Awake()
    {
        for (int i = 0; i < m_Collider.Length; ++i)
            m_Collider[i].enabled = false;
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public void OnSet(bool inSet)
    {
        if (inSet)
        {
            m_ParticleSystem.Play();
            for (int i = 0; i < m_Collider.Length; ++i)
                m_Collider[i].enabled = true;
        }
        else
        {
            m_ParticleSystem.Stop();
            for (int i = 0; i < m_Collider.Length; ++i)
                m_Collider[i].enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.CompareTag("Player")) 
            m_Contact?.Invoke(other);
    }
}
