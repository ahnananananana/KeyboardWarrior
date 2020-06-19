using System;

public enum StatModType
{
    Flat,           // 합연산
    PercentAdd,     // 곱연산을 합연산으로
    PercentMult,    // 곱연산을 곱연산으로
}

public class StatModifier
{
    public readonly float m_Value;
    public readonly StatModType m_Type;
    public readonly int m_Order;        // 곱연산/합연산을 순서대로 하기 위한 변수
    public readonly object m_Source;    // 특정 아이템이나 버프가 여러 스텟을 바꾸거나 합연산, 곱연산이 모두 존재하는 경우 이를 한 번에 제거해주기 위한 변수

    public StatModifier(float value, StatModType type, int order, object source)
    {
        m_Value = value;
        m_Type = type;
        m_Order = order;
        m_Source = source;
    }

    public StatModifier(float value, StatModType type)                  : this(value, type, (int)type, null)    { }
    public StatModifier(float value, StatModType type, int order)       : this(value, type, order, null)        { }
    public StatModifier(float value, StatModType type, object source)   : this(value, type, (int)type, source)  { }
}
