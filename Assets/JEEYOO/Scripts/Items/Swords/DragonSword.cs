using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSword : Sword
{
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(200, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
    }
}
