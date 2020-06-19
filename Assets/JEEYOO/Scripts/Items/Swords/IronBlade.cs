using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBlade : Sword
{
    private void Start()
    {
        m_ID = 1;
        m_ItemName = "IronBlade";
    }
    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(20, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
    }
}
