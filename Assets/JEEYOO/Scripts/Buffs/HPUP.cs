using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUP : Buff
{
    public override void ApplyBuff(Character c)
    {
        c.m_MaxHP.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public override void RemoveBuff(Character c)
    {
        c.m_MaxHP.RemoveAllModFromSource(this);
    }
}
