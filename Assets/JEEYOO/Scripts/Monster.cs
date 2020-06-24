using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    protected int m_MonsterEXP = 100;

    public int EXP { get => m_MonsterEXP;}

    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); // 부모스크립트의 Start실행(대한)
        InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStats()
    {
        m_MaxHP.m_BaseValue = 100;
        m_MaxMP.m_BaseValue = 0;

        m_CurrHP = m_MaxHP.m_CurrentValue;
        m_CurrMP = m_MaxMP.m_CurrentValue;

        m_Attack.m_BaseValue = 60;
        m_Defense.m_BaseValue = 20;

        m_Magic.m_BaseValue = 60;
        m_Resistance.m_BaseValue = 20;

        m_MoveSpeed.m_BaseValue = 20;
        m_AttackSpeed.m_BaseValue = 20;
    }
}
