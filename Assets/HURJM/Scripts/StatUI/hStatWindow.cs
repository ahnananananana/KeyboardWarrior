using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hStatWindow : MonoBehaviour
{
    private Character m_Player;//Delegate 등록용 혹은 그외 수치 받아오는 용
    [SerializeField]
    private Transform m_StatContainer;
    [SerializeField]
    private Transform m_EquipmentContainer;

    private hStatElement[] m_StatUIElementList;
    private hEquipmentSlot[] m_EquipmentSlotList;
    [SerializeField]
    private Canvas m_Canvas;

    public bool isActive => m_Canvas.enabled;

    public void Init(Character inPlayer)
    {
        m_StatUIElementList = m_StatContainer.GetComponentsInChildren<hStatElement>();
        m_EquipmentSlotList = m_EquipmentContainer.GetComponentsInChildren<hEquipmentSlot>();
        m_Player = inPlayer;
        RefreshUI();
    }
    public void SetWindow(bool inSet)
    {
        m_Canvas.enabled = inSet;
    }

    public void RefreshUI()
    {
        m_StatUIElementList[0].Set("레벨", m_Player.m_EXP.m_Level.ToString());
        m_StatUIElementList[1].Set("체력", m_Player.m_MaxHP.m_CurrentValue.ToString());
        m_StatUIElementList[2].Set("마나", m_Player.m_MaxMP.m_CurrentValue.ToString());
        m_StatUIElementList[3].Set("물리공격력", m_Player.m_Attack.m_CurrentValue.ToString());
        m_StatUIElementList[4].Set("마법공격력", m_Player.m_Magic.m_CurrentValue.ToString());
        m_StatUIElementList[5].Set("방어력", m_Player.m_Defense.m_CurrentValue.ToString());
        m_StatUIElementList[6].Set("저항", m_Player.m_Resistance.m_CurrentValue.ToString());
        m_StatUIElementList[7].Set("공격속도", m_Player.m_AttackSpeed.m_CurrentValue.ToString());
        m_StatUIElementList[8].Set("이동속도", m_Player.m_MoveSpeed.m_CurrentValue.ToString());
    }
}
