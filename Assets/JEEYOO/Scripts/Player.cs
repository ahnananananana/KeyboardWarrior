using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    ////
    [SerializeField]
    private hPlayerUI m_UI;
    [SerializeField]
    private Animator m_Animator;

    private void Awake()
    {
        m_EXP = new Experience();
        m_UI.Init(this);

        deadEvent += Dead;
        changeEvent += m_UI.RefreshUI;

        m_MaxHP.changeEvent += m_UI.RefreshUI;
        m_MaxMP.changeEvent += m_UI.RefreshUI;
        m_Attack.changeEvent += m_UI.RefreshUI;
        m_Defense.changeEvent += m_UI.RefreshUI;
        m_Magic.changeEvent += m_UI.RefreshUI;
        m_Resistance.changeEvent += m_UI.RefreshUI;
        m_MoveSpeed.changeEvent += m_UI.RefreshUI;
        m_AttackSpeed.changeEvent += m_UI.RefreshUI;
    }

    private void Dead()
    {
        m_Animator.SetTrigger("Dead");
        StartCoroutine(ShowGameOverPanel());
    }

    private IEnumerator ShowGameOverPanel()
    {
        Color color = m_UI.YOUDIED.color;
        Image youdied = m_UI.YOUDIED;
        while (color.a < .9f)
        {
            color.a += .01f;
            youdied.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        hLevelManager.current.GameOver();
    }
    ////
}
