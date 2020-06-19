using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBattleWindow : MonoBehaviour
{
    private Character m_Player;//Delegate 등록용 혹은 그외 수치 받아오는 용
    [SerializeField]
    private hStatBar m_HealthBar, m_ManaBar, m_ExpBar;
    [SerializeField]
    private hStatusContainer m_StatusContainer;
    [SerializeField]
    private Canvas m_Canvas;

    public void Init(Character inPlayer)
    {
        m_Player = inPlayer;
        RefreshUI();
    }

    public void SetActive(bool inSet) => m_Canvas.enabled = inSet;

    public void RefreshUI()
    {
        m_HealthBar.SetMax(m_Player.m_MaxHP.m_CurrentValue);
        m_HealthBar.SetValue(m_Player.m_CurrHP);
        m_ManaBar.SetMax(m_Player.m_MaxMP.m_CurrentValue);
        m_ManaBar.SetValue(m_Player.m_CurrMP);

        //m_ExpBar.SetMax();//현재 경험치 테이블이 필요
        m_ExpBar.SetValue(m_Player.m_EXP.m_CurrExp);
    }

}
