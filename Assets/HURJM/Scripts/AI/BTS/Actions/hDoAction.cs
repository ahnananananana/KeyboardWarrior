using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDoAction : hActionNode
{
    public delegate NodeState DelActionNode();

    private DelActionNode m_Action;
    public DelActionNode action { get => m_Action; set => m_Action = value; }

    public hDoAction(DelActionNode inAction) => m_Action = inAction;

    protected override NodeState Execute()
    {
         return m_Action();
    }
}
