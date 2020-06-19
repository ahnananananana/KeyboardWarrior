using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience
{
    public int m_Level = 1;
    public float m_CurrExp = 0;
    public float m_TotalExp = 0;
    public float m_BaseExp = 100;
    public float m_ExpModifier = 1.2f;

    public void GetExp(Monster m)
    {
        m_TotalExp += m.EXP;
        m_CurrExp += m.EXP;
        if(m_CurrExp >= m_BaseExp)
        {
            m_CurrExp = m_CurrExp - m_BaseExp;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        m_Level++;
        m_BaseExp *= m_ExpModifier;
    }
}
