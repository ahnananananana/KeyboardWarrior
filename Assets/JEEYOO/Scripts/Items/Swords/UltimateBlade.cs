using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateBlade : Sword
{
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(300, StatModType.Flat, this));
        c.m_MaxHP.AddModifier(new StatModifier(100, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
        c.m_MaxHP.RemoveAllModFromSource(this);
    }
}
