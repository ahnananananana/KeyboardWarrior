using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : Buff
{
    public override void ApplyBuff(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public override void RemoveBuff(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
    }
}