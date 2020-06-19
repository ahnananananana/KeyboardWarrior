using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hPlayer : Character
{
    [SerializeField]
    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    [SerializeField]
    private LayerMask m_CastLayer;
    private Vector3 m_Des;
    [SerializeField]
    private Transform m_MuzzlePos;
    [SerializeField]
    private GameObject m_ShootFlash, m_Bullet;
    private bool m_IsAttacking;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_ShootFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Shoot())
        {
        }
        if(Picking())
        {
            MoveTo();
        }

        IsArrive();
    }

    private void MoveTo()
    {
        m_Agent.speed = m_MoveSpeed.m_CurrentValue;
        m_Agent.SetDestination(m_Des);
    }

    private void IsArrive()
    {
        m_Animator.SetFloat("MoveSpeed", m_Agent.speed);
        if (Vector3.Distance(transform.position, m_Des) < .1f)
        {
            Debug.Log("Stop");
            m_Agent.velocity = Vector3.zero;
            m_Agent.speed = 0f;
            m_Agent.ResetPath();
        }
    }

    bool Picking()
    {
        if (!Input.GetMouseButton(1)) return false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, m_CastLayer))
        {
            Vector3 pos = transform.position;
            pos.x = hit.point.x;
            pos.z = hit.point.z;
            m_Des = pos;
            return true;
        }
        return false;
    }

    private bool Shoot()
    {
        if (!Input.GetMouseButton(0))
            return false;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, m_CastLayer))
        {
            Vector3 pos = transform.position;
            pos.x = hit.point.x;
            pos.z = hit.point.z;
            transform.LookAt(pos);
        }

        m_Agent.velocity = Vector3.zero;
        m_Agent.speed = 0f;
        m_Agent.ResetPath();
        m_Animator.SetFloat("AttackSpeed", m_AttackSpeed.m_CurrentValue);
        m_Animator.SetTrigger("Attack");
        m_IsAttacking = true;

        return true;
    }

    public void ShootBullet()
    {
        Debug.Log("Shoot");
        m_Animator.ResetTrigger("Attack");
        m_ShootFlash.SetActive(true);
        hBullet bullet = Instantiate(m_Bullet, m_MuzzlePos.position, m_MuzzlePos.rotation).GetComponent<hBullet>();
        bullet.shooter = this;
    }

    public void ShootEnd()
    {
        Debug.Log("ShootEnd");
        m_ShootFlash.SetActive(false);
        m_IsAttacking = false;
    }

}
