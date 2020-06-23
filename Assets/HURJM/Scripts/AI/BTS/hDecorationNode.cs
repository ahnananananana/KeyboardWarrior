using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool DelBool();

public class hDecorationNode : hBTSNode
{
    private hBTSNode m_Child;

    private DelBool m_Condition;
    private bool m_IsWatch;

    public hBTSNode child { set => m_Child = value; }
    public DelBool condition { get => m_Condition; set => m_Condition = value; }

    public hDecorationNode(bool inIsWatch = true) => m_IsWatch = inIsWatch;

    public hDecorationNode(hBTSNode inChild, DelBool inDel, bool inIsWatch = true)
    {
        m_Child = inChild;
        m_Condition = inDel;
    }

    public hDecorationNode(DelBool inDel, bool inIsWatch = true)
    {
        m_Condition = inDel;
        m_IsWatch = inIsWatch;
    }

    public override NodeState Evaluate()
    {
        bool condition = m_Condition();
        if (!m_IsWatch && m_State == NodeState.RUNNING)
            condition = true;

        if (condition)
        {
            m_State = m_Child.Evaluate();
            return m_State;
        }
        else
        {
            m_Child.ResetState();
            m_State = NodeState.FAILURE;
            return m_State;
        }
    }

    public override void ResetState()
    {
        m_State = NodeState.NONE;
        m_Child.ResetState();
    }
}
