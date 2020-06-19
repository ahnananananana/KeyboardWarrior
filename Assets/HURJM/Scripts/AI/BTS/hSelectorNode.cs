using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class hSelectorNode : hBTSNode
{
    private List<hBTSNode> m_Children;
    private int m_PreRunningChildIndex;

    public hSelectorNode() => m_Children = new List<hBTSNode>();

    public hSelectorNode(List<hBTSNode> inChildren)
    {
        m_Children = inChildren;
    }

    public List<hBTSNode> children { get => m_Children; set => m_Children = value; }

    public override NodeState Evaluate()
    {
        int index = 0, count = 0;
        if (m_State == NodeState.RUNNING)
            index = m_PreRunningChildIndex;

        for (int i = index; i < m_Children.Count; ++i)
        {
            if(count >= m_Children.Count)
                break;
            ++count;
            i %= m_Children.Count;
            switch (m_Children[i].Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    m_State = NodeState.SUCCESS;
                    return m_State;
                case NodeState.RUNNING:
                    m_State = NodeState.RUNNING;
                    m_PreRunningChildIndex = i;
                    return m_State;
                default:
                    continue;
            }
        }

        m_State = NodeState.FAILURE;
        return m_State;
    }

    public override void ResetState()
    {
        m_State = NodeState.NONE;
        for (int i = 0; i < m_Children.Count; ++i)
            m_Children[i].ResetState();
    }
}
