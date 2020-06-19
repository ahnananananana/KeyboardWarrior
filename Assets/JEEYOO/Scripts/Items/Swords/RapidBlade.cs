using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidBlade : Sword
{
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(60, StatModType.Flat, this));
        c.m_AttackSpeed.AddModifier(new StatModifier(20, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
        c.m_AttackSpeed.RemoveAllModFromSource(this);
    }
}
