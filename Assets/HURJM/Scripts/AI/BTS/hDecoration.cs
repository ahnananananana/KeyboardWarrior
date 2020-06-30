using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operation
{
    EQUAL,
    GREAT,
    LESS,
    GREATEQUAL,
    LESSEQUAL,
}

public class hDecoration<T>
{
    private T m_Condition;
    private Operation m_Operation;

    public hDecoration(T inCondition, Operation inOperation)
    {
        m_Condition = inCondition;
        m_Operation = inOperation;
    }

    
}
