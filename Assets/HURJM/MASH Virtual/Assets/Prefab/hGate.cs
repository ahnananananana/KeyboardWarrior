using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hGate : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    public void Open() => m_Animator.SetTrigger("Open");
    public void Close() => m_Animator.SetTrigger("Close");
}
