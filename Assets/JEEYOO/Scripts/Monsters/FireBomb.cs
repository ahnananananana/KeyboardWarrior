﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FireBomb : Monster
{
    private Transform m_Transform;
    private Transform playerTransform;
    private NavMeshAgent m_NVAgent;
    private Animator m_Animator;
    

    private float m_TraceDist = 20f;
    private float m_AttackDist = 15f;

    private Vector3 firePosition;

    private bool isDead = false;


    // Start is called before the first frame update
    new void Start()
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

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(m_Transform.position, playerTransform.position);

            if (dist <= m_AttackDist)
            {
                m_state = STATE.ATTACK;
            }
            else if (dist <= m_TraceDist)
            {
                m_state = STATE.TRACE;
            }
            else if(CurrHP <= 0)
            {
                m_state = STATE.DEAD;
            }
            else
            {
                m_state = STATE.IDLE;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch(m_state)
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
                    transform.LookAt(playerTransform);
                    m_Animator.SetBool("isAttack", true);
                    break;
                case STATE.DAMAGED:
                    m_Animator.SetBool("isDamaged", true);
                    break;
                case STATE.DEAD:
                    m_Animator.SetTrigger("isDead");
                    isDead = true;
                    GetComponent<Collider>().enabled = false;
                    break;
            }
            yield return null;
        }
    }

    private void InitStat()
    {
        m_MaxHP.m_BaseValue = 100;
        m_MaxMP.m_BaseValue = 0;
        m_CurrHP = 100;

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
