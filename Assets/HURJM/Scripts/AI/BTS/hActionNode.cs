using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class hActionNode : hBTSNode
{
    public override NodeState Evaluate()
    {
        switch(Execute())
        {
            case NodeState.SUCCESS:
                m_State = NodeState.SUCCESS;
                return m_State;
            case NodeState.FAILURE:
                m_State = NodeState.FAILURE;
                return m_State;
            case NodeState.RUNNING:
                m_State = NodeState.RUNNING;
                return m_State;
            default:
                m_State = NodeState.FAILURE;
                return m_State;
        }
    }

    protected abstract NodeState Execute();

    public override void ResetState()
    {
        m_State = NodeState.NONE;
    }
}
