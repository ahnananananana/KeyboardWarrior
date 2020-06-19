using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    NONE,
    FAILURE,
    SUCCESS,
    RUNNING,
}

public delegate NodeState DelBTS();

[System.Serializable]
public abstract class hBTSNode
{
    public delegate NodeState NodeReturn();

    protected NodeState m_State;

    public NodeState state => m_State;

    public hBTSNode() { }

    public abstract void ResetState();
    public abstract NodeState Evaluate();
}
