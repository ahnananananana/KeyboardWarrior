using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hPlayerUI : MonoBehaviour
{
    [SerializeField]
    private Player m_Player;
    [SerializeField]
    private hStatWindow m_StatWindow;
    [SerializeField]
    private hBattleWindow m_BattleWindow;
    [SerializeField]
    private Image m_YOUDIED;

    public Image YOUDIED { get => m_YOUDIED;}

    public void Init(Player inPlayer)
    {
        m_Player = inPlayer;
        m_StatWindow.Init(m_Player);
        m_StatWindow.SetWindow(false);
        m_BattleWindow.Init(m_Player);
    }

    public void ShowStatWindow()
    {
        if (!m_StatWindow.isActive)
            m_StatWindow.SetWindow(true);
        else
            m_StatWindow.SetWindow(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!m_StatWindow.isActive)
                m_StatWindow.SetWindow(true);
            else
                m_StatWindow.SetWindow(false);
        }

    }

    public void RefreshUI()
    {
        m_BattleWindow.RefreshUI();
        m_StatWindow.RefreshUI();
    }
}
