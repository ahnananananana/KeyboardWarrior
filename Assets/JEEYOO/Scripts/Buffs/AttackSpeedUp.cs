using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : Buff
{
    public override void ApplyBuff(Character c)
    {
        c.m_AttackSpeed.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public override void RemoveBuff(Character c)
    {
        c.m_AttackSpeed.RemoveAllModFromSource(this);
    }
}
