using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FireBomb : Monster
{
    public enum STATE
    {
        IDLE, TRACE, ATTACK, DEAD,
    }

    public STATE m_State = STATE.IDLE;

    private Transform m_Transform;
    private Transform playerTransform;
    private NavMeshAgent m_NVAgent;
    private Animator m_Animator;
    

    private float m_TraceDist = 20f;
    private float m_AttackDist = 15f;

    private Vector3 firePosition;

    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        InitStat();
        m_Transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        m_NVAgent = this.gameObject.GetComponent<NavMeshAgent>();
        m_Animator = this.gameObject.GetComponent<Animator>();
        

        m_NVAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(m_Transform.position, playerTransform.position);

            if (dist <= m_AttackDist)
            {
                m_State = STATE.ATTACK;
            }
            else if (dist <= m_TraceDist)
            {
                m_State = STATE.TRACE;
            }
            else
            {
                m_State = STATE.IDLE;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch(m_State)
            {
                case STATE.IDLE:
                    m_NVAgent.Stop();
                    m_Animator.SetBool("isTrace", false);
                    break;
                case STATE.TRACE:
                    m_NVAgent.destination = playerTransform.position;
                    m_NVAgent.Resume();
                    m_Animator.SetBool("isTrace", true);
                    break;
                case STATE.ATTACK:
                    m_NVAgent.Stop();
                    m_Animator.SetBool("isAttack", true);
                    break;
                case STATE.DEAD:
                    break;
            }
            yield return null;
        }
    }

    private void InitStat()
    {
        m_MaxHP.m_BaseValue = 100;
        m_MaxMP.m_BaseValue = 0;

        m_Attack.m_BaseValue = 50;
        m_Defense.m_BaseValue = 20;
        m_Magic.m_BaseValue = 100;
        m_Resistance.m_BaseValue = 40;

        m_MoveSpeed.m_BaseValue = 20;
        m_AttackSpeed.m_BaseValue = 20;

        m_MonsterEXP = 100;
    }

    private void FireProjectile()
    {
        
        GameObject projectile = Instantiate((GameObject)Resources.Load("Prefabs/explode_2")) as GameObject;
        projectile.transform.position = this.gameObject.transform.position;
    }
}
