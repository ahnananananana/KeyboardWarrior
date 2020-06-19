using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hSequenceNode : hBTSNode
{
    private List<hBTSNode> m_Children;
    private int m_PreRunningChildIndex;

    public hSequenceNode() => m_Children = new List<hBTSNode>();

    public hSequenceNode(List<hBTSNode> inChildren)
    {
        m_Children = inChildren;
    }

    public List<hBTSNode> children { get => m_Children; set => m_Children = value; }

    public override NodeState Evaluate()
    {
        int index = 0;
        if (m_State == NodeState.RUNNING)
            index = m_PreRunningChildIndex;

        for (int i = index; i < m_Children.Count; ++i)
        {
            switch(m_Children[i].Evaluate())
            {
                case NodeState.FAILURE:
                    m_State = NodeState.FAILURE;
                    return m_State;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    m_State = NodeState.RUNNING;
                    m_PreRunningChildIndex = i;
                    return m_State;
                default:
                    m_State = NodeState.SUCCESS;
                    return m_State;
            }
        }

        m_State = NodeState.SUCCESS;
        return m_State;
    }

    public override void ResetState()
    {
        m_State = NodeState.NONE;
        for (int i = 0; i < m_Children.Count; ++i)
            m_Children[i].ResetState();
    }
}
