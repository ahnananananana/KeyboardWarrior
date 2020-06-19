using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int m_ID;
    public string m_ItemName;

    public virtual void Equip(Character c) { }
    public virtual void UnEquip(Character c) { }
}
