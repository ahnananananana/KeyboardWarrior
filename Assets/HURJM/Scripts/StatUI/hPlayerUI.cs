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
    [SerializeField]
    private hBuffPopup m_BuffPopup;
    [SerializeField]
    private Transform m_BuffListContainer;
    [SerializeField]
    private hBuffListUIElement m_BuffListUIElementPrefab;
    private List<hBuffListUIElement> m_BuffUIList;

    public Image YOUDIED { get => m_YOUDIED;}

    private void Awake()
    {
        m_BuffPopup.gameObject.SetActive(false);
        m_BuffUIList = new List<hBuffListUIElement>();
        hLevelManager.current.buffList.addEvent += AddBuffListUI;
        hLevelManager.current.buffList.removeEvent += RemoveBuffListUI;

        for (int i = 0; i < hLevelManager.current.buffList.Count; ++i)
            AddBuffListUI(hLevelManager.current.buffList[i]);
    }

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

    public void SetBuffPopup(Buff inBuff)
    {
        m_BuffPopup.gameObject.SetActive(true);
        m_BuffPopup.Set(inBuff);
        hLevelManager.current.buffList.Add(inBuff);
    }

    public void RefreshUI()
    {
        m_BattleWindow.RefreshUI();
        m_StatWindow.RefreshUI();
    }

    private void AddBuffListUI(Buff inBuff)
    {
        var element = Instantiate(m_BuffListUIElementPrefab);
        element.Set(inBuff);
        element.transform.SetParent(m_BuffListContainer);
        element.transform.localRotation = Quaternion.identity;
        element.transform.localScale = Vector3.one;
        element.transform.localPosition = Vector3.zero;
        m_BuffUIList.Add(element);
    }

    private void RemoveBuffListUI(Buff inBuff)
    {
        for (int i = 0; i < m_BuffUIList.Count; ++i)
        {
            var element = m_BuffUIList[i];
            if(element.buff == inBuff)
            {
                m_BuffUIList.Remove(element);
                DestroyImmediate(element.gameObject);
                break;
            }
        }
    }
}
