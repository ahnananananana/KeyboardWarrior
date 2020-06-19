using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hZombieAI : hMonsterAI
{
    protected override void InitBTS()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_RootNode = new hSelectorNode();
        hSequenceNode subRoot = new hSequenceNode();
        m_RootNode.children.Add(subRoot);

        hSearchAndFollowAction searchAndFollow = new hSearchAndFollowAction(this, m_Target.transform, null, m_AttackRange);
        hActionNode attack = new hDoAnimation(this, "Attack", m_Target.transform);

        hSequenceNode pattern = new hSequenceNode();
        pattern.children.Add(searchAndFollow);
        pattern.children.Add(attack);


        subRoot.children.Add(pattern);
    }

    public void GiveDamage()
    {
        if (Vector3.Distance(transform.position, m_Target.transform.position) < m_AttackRange)
        {
            Debug.Log("attack");
            m_Character.DealDamage(m_Target);
        }
    }

    protected override void Dead()
    {
        m_Collider.enabled = false;
        m_Agent.isStopped = true;
        m_Animator.SetTrigger("Dead");
        m_launch = false;
    }
}
