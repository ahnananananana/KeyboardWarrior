using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStatWindowPlayer : MonoBehaviour
{
    private CharCtrl m_Player;
    [SerializeField]
    public Transform m_BowTR, m_GunTR, m_SwordTR;
    [SerializeField]
    private RuntimeAnimatorController m_BowAni, m_GunAni, m_SwordAni;
    [SerializeField]
    private GameObject m_Bow_Obj, m_Gun_Obj, m_Sword_Obj;
    private GameObject m_WeaponObj;
    private Animator m_Animator;
    private CharCtrl.WEAPONTYPE m_WeaponType;
    private void Start()
    {
        m_Player = FindObjectOfType<CharCtrl>();
        m_Animator = GetComponent<Animator>();

        if (m_Player.UsingWeaponTR == m_Player.BowTR)
        {
            m_Animator.runtimeAnimatorController = m_BowAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.BOW;
            SetWeapon(m_Bow_Obj, m_BowTR);
        }
        else if (m_Player.UsingWeaponTR == m_Player.GunTR)
        {
            m_Animator.runtimeAnimatorController = m_GunAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.GUN;
            SetWeapon(m_Gun_Obj, m_GunTR);
        }
        else if (m_Player.UsingWeaponTR == m_Player.SwordTR)
        {
            m_Animator.runtimeAnimatorController = m_SwordAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.SWORD;
            SetWeapon(m_Sword_Obj, m_SwordTR);
        }
    }

    private void Update()
    {
        if(m_Player.UsingWeaponTR == m_Player.BowTR && m_WeaponType != CharCtrl.WEAPONTYPE.BOW)
        {
            m_Animator.runtimeAnimatorController = m_BowAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.BOW;
            SetWeapon(m_Bow_Obj, m_BowTR);
        }
        else if(m_Player.UsingWeaponTR == m_Player.GunTR && m_WeaponType != CharCtrl.WEAPONTYPE.GUN)
        {
            m_Animator.runtimeAnimatorController = m_GunAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.GUN;
            SetWeapon(m_Gun_Obj, m_GunTR);
        }
        else if(m_Player.UsingWeaponTR == m_Player.SwordTR && m_WeaponType != CharCtrl.WEAPONTYPE.SWORD)
        {
            m_Animator.runtimeAnimatorController = m_SwordAni;
            m_WeaponType = CharCtrl.WEAPONTYPE.SWORD;
            SetWeapon(m_Sword_Obj, m_SwordTR);
        }
    }

    private void SetWeapon(GameObject inWeapon, Transform inWeaponTR)
    {
        if (m_WeaponObj != null) 
            DestroyImmediate(m_WeaponObj);

        m_WeaponObj = Instantiate(inWeapon) as GameObject;
        m_WeaponObj.transform.SetParent(inWeaponTR);
        m_WeaponObj.transform.position = inWeaponTR.position;
        m_WeaponObj.transform.rotation = inWeaponTR.rotation;
        m_WeaponObj.layer = LayerMask.NameToLayer("PlayerForStat");
        foreach(Transform child in m_WeaponObj.transform)
            child.gameObject.layer = LayerMask.NameToLayer("PlayerForStat");
    }
}
