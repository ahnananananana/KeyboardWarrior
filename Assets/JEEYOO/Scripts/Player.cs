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

    protected override void Awake()
    {
        base.Awake();
        InitStats();
        m_EXP = new Experience();

        deadEvent += Dead;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("파이클");
    }


    public void AttachUI(hPlayerUI inUI)
    {
        m_UI = inUI;

        m_UI.Init(this);
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

    new void Start()
    {
        
    }

    private void InitStats()
    {
        m_MaxHP.m_BaseValue = 500;
        m_MaxMP.m_BaseValue = 100;

        m_CurrHP = m_MaxHP.m_CurrentValue;
        m_CurrMP = m_MaxMP.m_CurrentValue;

        m_Attack.m_BaseValue = 80;
        m_Defense.m_BaseValue = 20;

        m_Magic.m_BaseValue = 80;
        m_Resistance.m_BaseValue = 20;

        m_MoveSpeed.m_BaseValue = 20;
        m_AttackSpeed.m_BaseValue = 20;
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
