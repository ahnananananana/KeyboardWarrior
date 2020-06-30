using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelT<T>(T inItem);

public class hFlameBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CastRange, m_AttackRange;
    private event DelCollider m_TriggerEvent;
    private event DelT<hFlameBomb> m_EndEvent;
    [SerializeField]
    private float m_CastTime;
    private float m_StartTime;
    [SerializeField]
    private Character m_Character;
    private Collider m_Collider;
    [SerializeField]
    private hParticleSystem m_Particle;

    public Character character { get => m_Character; set => m_Character = value; }
    public float castTime { get => m_CastTime; set => m_CastTime = value; }

    public event DelCollider triggerEvent { add { m_TriggerEvent += value; } remove { m_TriggerEvent -= value; } }
    public event DelT<hFlameBomb> endEvent { add { m_EndEvent += value; } remove { m_EndEvent -= value; } }

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
        m_CastRange.transform.localScale = Vector3.zero;
        m_StartTime = Time.time;
        m_Particle.endEvent += () => { m_EndEvent?.Invoke(this); };
    }

    private void Start() => StartCoroutine(StartCast());

    public void SetPos(Vector3 inPos) => transform.position = inPos;

    private IEnumerator StartCast()
    {
        float curTime;
        do
        {
            curTime = Time.time - m_StartTime;
            m_CastRange.transform.localScale = Vector3.one * curTime / m_CastTime;
            yield return null;
        } while (curTime < m_CastTime);

        m_CastRange.SetActive(false);
        m_AttackRange.SetActive(false);
        m_Collider.enabled = true;
        //play particle
        m_Particle.Play();
    }
    public void SetRangeScale(float inScale) => m_AttackRange.transform.localScale = Vector3.one * inScale;
    private void OnTriggerStay(Collider other) => m_TriggerEvent?.Invoke(other);

}
