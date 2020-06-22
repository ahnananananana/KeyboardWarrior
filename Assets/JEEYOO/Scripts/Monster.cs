using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    protected int m_MonsterEXP = 100;

    public int EXP { get => m_MonsterEXP;}

    // Start is called before the first frame update
    void Start()
    {
        m_CurrHP = m_MaxHP.m_CurrentValue;
        m_CurrMP = m_MaxMP.m_CurrentValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
