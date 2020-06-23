﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResUp : Buff
{
    public override void ApplyBuff(Character c)
    {
        c.m_Resistance.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public override void RemoveBuff(Character c)
    {
        c.m_Resistance.RemoveAllModFromSource(this);
    }
}
