using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//movable target search follow
public class hSearchAndFollowAction : hAiAction
{
    private Animator m_Animator;
    private Transform m_Target;
    private NavMeshAgent m_Agent;
    private Vector3 m_TargetPos, m_PreTargetPos;
    private Rigidbody m_RigidBody;

    private int m_VerticalSpeedHash, m_HorizontalSpeedHash, m_DirectionHash;
    private Transform m_LookAt;
    private float m_StopDistance;
    private bool m_IsTargetMoved;

    public hSearchAndFollowAction(hMonsterAI inMonster, Transform inTarget, Transform inLookAt = null, float inStopDistance = .1f)
    {
        m_Monster = inMonster;
        m_Target = inTarget;
        m_Agent = m_Monster.agent;
        m_RigidBody = m_Monster.rigidBody;

        m_PreTargetPos = m_Monster.transform.position;

        m_Animator = m_Monster.animator;
        m_VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        m_HorizontalSpeedHash = Animator.StringToHash("HorizontalSpeed");
        m_DirectionHash = Animator.StringToHash("Direction");
        m_Agent = m_Monster.agent;
        m_StopDistance = inStopDistance;
        m_LookAt = inLookAt;

        m_IsTargetMoved = true;
    }

    protected override NodeState Execute()
    {
        if (m_IsTargetMoved)
        {
            m_IsTargetMoved = false;
            m_TargetPos = m_Target.position;
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            m_PreTargetPos = m_TargetPos;
            m_Agent.speed = m_Monster.character.m_MoveSpeed.m_CurrentValue;
            m_Agent.SetDestination(m_TargetPos);
            return NodeState.RUNNING;
        }

        if (m_Agent.pathStatus == NavMeshPathStatus.PathPartial || m_Agent.pathStatus == NavMeshPathStatus.PathInvalid) 
            return NodeState.FAILURE;

        if (m_Agent.hasPath)
        {
            if (m_LookAt != null)
                m_Monster.root.LookAt(m_LookAt);

            Vector3 moveDirection = m_Monster.transform.forward;
            Vector3 lookDirection = m_Monster.root.forward;

            if (Vector3.Distance(m_Monster.transform.position, m_Target.position) < m_StopDistance)
            {
                if (m_LookAt != null)
                    m_Agent.transform.LookAt(m_Monster.root);
                m_Agent.velocity = Vector3.zero;
                m_Agent.speed = 0f;
                m_Agent.ResetPath();
                m_Animator.SetFloat(m_VerticalSpeedHash, 0f);
                m_Animator.SetFloat(m_HorizontalSpeedHash, 0f);
                m_Animator.SetFloat(m_DirectionHash, Vector3.Dot(moveDirection, lookDirection));
                m_IsTargetMoved = true;
                return NodeState.SUCCESS;
            }

            float verticalSpeed = Mathf.Abs(m_Agent.velocity.z);
            float horizontalSpeed = Mathf.Abs(m_Agent.velocity.x);

            m_Animator.SetFloat(m_VerticalSpeedHash, verticalSpeed);
            m_Animator.SetFloat(m_HorizontalSpeedHash, horizontalSpeed);
            m_Animator.SetFloat(m_DirectionHash, Vector3.Dot(moveDirection, lookDirection));

            if (m_Target.position != m_PreTargetPos)
                m_IsTargetMoved = true;

            return NodeState.RUNNING;
        }
        return NodeState.RUNNING;
    }
}
