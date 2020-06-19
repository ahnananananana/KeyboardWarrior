﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingSword : Sword
{
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(150, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
    }
}
