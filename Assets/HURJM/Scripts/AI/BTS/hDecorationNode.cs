using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDecorationNode : hBTSNode
{
    public delegate bool DelDecoration();

    private hBTSNode m_Child;

    private DelDecoration m_Decoration;
    private bool m_IsWatch;

    public hBTSNode child { set => m_Child = value; }

    public hDecorationNode(hBTSNode inChild, DelDecoration inDel, bool inIsWatch = true)
    {
        m_Child = inChild;
        m_Decoration = inDel;
    }

    public hDecorationNode(DelDecoration inDel, bool inIsWatch = true)
    {
        m_Decoration = inDel;
        m_IsWatch = inIsWatch;
    }


    public override NodeState Evaluate()
    {
        bool condition = m_Decoration();
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
