using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadSword : Sword
{
    void Start()
    {
        m_ID = 0;
        m_ItemName = "브로드소드";
    }

    public override void Equip(Character c)
    {
        c.m_Attack.AddModifier(new StatModifier(10, StatModType.Flat, this));
    }

    public override void UnEquip(Character c)
    {
        c.m_Attack.RemoveAllModFromSource(this);
    }
}
