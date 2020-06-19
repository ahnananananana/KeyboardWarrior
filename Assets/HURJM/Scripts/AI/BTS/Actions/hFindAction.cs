using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hFindAction : hAiAction
{
    private Animator m_Animator;
    private Transform m_Target;
    private NavMeshAgent m_Agent;
    private Vector3 m_TargetPos, m_PreTargetPos;
    private Rigidbody m_RigidBody;

    public hFindAction(hMonsterAI inMonster, Transform inTarget)
    {
        m_Monster = inMonster;
        m_Target = inTarget;
        m_Agent = m_Monster.agent;
        m_RigidBody = m_Monster.rigidBody;
        m_Animator = m_Monster.GetComponent<Animator>();

        m_PreTargetPos = m_Monster.transform.position;
    }

    protected override NodeState Execute()
    {
        Debug.Log(m_Agent.hasPath);
        if(m_State == NodeState.RUNNING)
        {
            if (m_Agent.hasPath) return NodeState.SUCCESS;
            else if (m_Agent.pathStatus != NavMeshPathStatus.PathPartial) return NodeState.FAILURE;
            else return NodeState.RUNNING;
        }

        m_TargetPos = m_Target.position;
        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        m_PreTargetPos = m_TargetPos;
        m_Agent.speed = m_Monster.character.m_MoveSpeed.m_CurrentValue;
        m_Agent.SetDestination(m_TargetPos);
        m_Animator.SetFloat("VerticalSpeed", 1f);
        return NodeState.RUNNING;

       /* if (Vector3.Distance(m_TargetPos, m_PreTargetPos) > .01f)//타겟의 위치가 달라진다면
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            m_PreTargetPos = m_TargetPos;
            m_Agent.speed = m_Monster.character.m_MoveSpeed.m_CurrentValue;
            m_Agent.SetDestination(m_TargetPos);
            return NodeState.RUNNING;
        }*/
    }
}
