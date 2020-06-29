using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public Sprite icon;
    public string buffName;

    public virtual void ApplyBuff(Character c) { }

    public virtual void RemoveBuff(Character c) { }
    
}
