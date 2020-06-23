using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum STATE
    {
        CREATE, ALIVE, DEAD,
    }

    public event DelVoid changeEvent;////
    public event DelVoid deadEvent;

    public STATE m_state = STATE.ALIVE;
    public int m_ID;
    public string m_CharName;

    public Stat m_MaxHP;
    public Stat m_MaxMP;
    public float m_CurrHP;
    public float m_CurrMP;

    public Stat m_Attack;
    public Stat m_Defense;
    public Stat m_Magic;
    public Stat m_Resistance;

    public Stat m_MoveSpeed;
    public Stat m_AttackSpeed;

    public Experience m_EXP = new Experience();
    public float m_Damage;

    protected virtual void Start()
    {
        
    }

    public float CurrHP////
    {
        get => m_CurrHP;
        set
        {
            m_CurrHP = value;
            changeEvent?.Invoke();
        }
    }
    public float CurrMP////
    {
        get => m_CurrMP;
        set
        {
            m_CurrMP = value;
            changeEvent?.Invoke();
        }
    }
    public float Exp////
    {
        get => m_EXP.m_CurrExp;
        set
        {
            m_EXP.m_CurrExp = value;
            changeEvent?.Invoke();
        }
    }
    public int Level////
    {
        get => m_EXP.m_Level;
        set
        {
            m_EXP.m_Level = value;
            changeEvent?.Invoke();
        }
    }


    public void IncreaseStatByLvUp()
    {
        m_MaxHP.IncreaseBaseValue(1.2f);
        m_MaxMP.IncreaseBaseValue(1.2f);
        m_Attack.IncreaseBaseValue(1.2f);
        m_Defense.IncreaseBaseValue(1.2f);
        m_Magic.IncreaseBaseValue(1.2f);
        m_Resistance.IncreaseBaseValue(1.2f);
        m_MoveSpeed.IncreaseBaseValue(1.05f);
        m_AttackSpeed.IncreaseBaseValue(1.05f);
    }

    public void DealDamage(Character defender, float inDamage = 0f)
    {
        if(inDamage > 0)
            m_Damage = (Random.Range(0.95f, 1.05f) * inDamage - defender.m_Defense.m_CurrentValue);
        else
            m_Damage = (Random.Range(0.95f, 1.05f) * m_Attack.m_CurrentValue - defender.m_Defense.m_CurrentValue);
        if (m_Damage < 0)
            m_Damage = 0;

        defender.CurrHP -= m_Damage;
        if (defender.m_state != STATE.DEAD && defender.CurrHP <= 0)////
        {
            if(defender.CompareTag("Monster"))
                m_EXP.GetExp((Monster)defender);
            defender.deadEvent?.Invoke();////
            defender.ChangeState(STATE.DEAD);
            defender.StateProcess();
        }
    }

    public void ChangeState(STATE s)
    {
        if (m_state == s) return;
        m_state = s;

        switch (s)
        {
            case STATE.CREATE:
                m_state = STATE.ALIVE;
                break;
            case STATE.ALIVE:
                break;
            case STATE.DEAD:
                break;
        }
    }

    public void StateProcess()
    {
        switch (m_state)
        {
            case STATE.CREATE:
                break;
            case STATE.ALIVE:
                break;
            case STATE.DEAD:
                //Destroy(this.gameObject);
                break;
        }
    }
}
