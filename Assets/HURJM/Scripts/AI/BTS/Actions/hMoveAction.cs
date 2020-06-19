using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hMoveAction : hAiAction
{
    private Animator m_Animator;
    private NavMeshAgent m_Agent;
    private int m_VerticalSpeedHash, m_HorizontalSpeedHash, m_DirectionHash;
    private Transform m_Destination;
    private Transform m_LookAt;
    private float m_StopDistance;

    public hMoveAction(hMonsterAI inMonster, Transform inDestination, Transform inLookAt = null, float inStopDistance = .1f)
    {
        m_Monster = inMonster;
        m_Animator = m_Monster.animator;
        m_VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        m_HorizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
        m_DirectionHash = Animator.StringToHash("Direction");
        m_Agent = m_Monster.agent;
        m_Destination = inDestination;
        m_StopDistance = inStopDistance;
        m_LookAt = inLookAt;
    }

    protected override NodeState Execute()
    {
        if (m_LookAt != null)
            m_Monster.root.LookAt(m_LookAt);

        Vector3 moveDirection = m_Monster.transform.forward;
        Vector3 lookDirection = m_Monster.root.forward;

        if (Vector3.Distance(m_Monster.transform.position, m_Destination.position) < m_StopDistance)
        {
            if (m_LookAt != null)
                m_Agent.transform.LookAt(m_Monster.root);
            m_Agent.velocity = Vector3.zero;
            m_Agent.speed = 0f;
            m_Agent.ResetPath();
            m_Animator.SetFloat(m_VerticalSpeedHash, 0f);
            m_Animator.SetFloat(m_HorizontalSpeedHash, 0f);
            m_Animator.SetFloat(m_DirectionHash, Vector3.Dot(moveDirection, lookDirection));
            return NodeState.SUCCESS;
        }

        float verticalSpeed = Mathf.Abs(m_Agent.velocity.z);
        float horizontalSpeed = Mathf.Abs(m_Agent.velocity.x);

        m_Animator.SetFloat(m_VerticalSpeedHash, verticalSpeed);
        m_Animator.SetFloat(m_HorizontalSpeedHash, horizontalSpeed);
        m_Animator.SetFloat(m_DirectionHash, Vector3.Dot(moveDirection, lookDirection));

        return NodeState.RUNNING;
    }
}
