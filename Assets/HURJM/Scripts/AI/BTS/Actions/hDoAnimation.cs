using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDoAnimation : hAiAction
{
    private Animator m_Animator;
    private int m_AnimationHash;
    private string m_AnimationName;
    private Transform m_LookAt;

    public hDoAnimation(hMonsterAI inMonster, string inAnimationName, Transform inLookAt = null)
    {
        m_Monster = inMonster;
        m_Animator = m_Monster.animator;
        m_AnimationName = inAnimationName;
        m_AnimationHash = Animator.StringToHash(inAnimationName);
        m_LookAt = inLookAt;
    }

    protected override NodeState Execute()
    {
        if (m_LookAt != null)
            m_Monster.root.LookAt(m_LookAt);//need coroutine

        if(m_State == NodeState.RUNNING)
        {
            if (m_Animator.IsInTransition(0) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(m_AnimationName))
            {
                return NodeState.SUCCESS;
            }
            else
            {
                //m_Animator.SetBool(m_AnimationHash, false);
                return NodeState.RUNNING;
            }
        }
        else
        {
            m_Animator.SetTrigger(m_AnimationHash);
            return NodeState.RUNNING;
        }

        /*if (!m_Run)
        {
            m_Animator.SetBool(m_AnimationHash, true);
            m_Run = true;
            return NodeState.RUNNING;
        }
        else if (!m_Animator.IsInTransition(0) && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName(m_AnimationName))
        {
            m_Run = false;
            return NodeState.SUCCESS;
        }

        m_Animator.SetBool(m_AnimationHash, false);
        return NodeState.RUNNING;*/
    }
}
