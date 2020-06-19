using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSword : Sword
{
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(120, StatModType.Flat, this));
        c.m_Magic.AddModifier(new StatModifier(30, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
        c.m_Magic.RemoveAllModFromSource(this);
    }
}
