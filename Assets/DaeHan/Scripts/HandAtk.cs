using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAtk : MonoBehaviour
{
    public enum STATE
    {
        CREATE, WORK, CRASH, DELETE
    }
    STATE state = STATE.CREATE;

    public float fTime = 0.0f;
    public float fDeleteTime = 0.1f;
    public GameObject Obj_CrashEffect;
    public GameObject CrashMonster;
    Vector3 EffectPos;

    Character m_character;////

    public Character character { get => m_character; set => m_character = value; }

    private void Update()
    {
        StateProcess();
    }

    void StateProcess()
    {
        switch(state)
        {
            case STATE.CREATE:
                ChangeState(STATE.WORK);
                break;
            case STATE.WORK:
                fTime += Time.smoothDeltaTime;
                if (fTime >= fDeleteTime)
                    ChangeState(STATE.DELETE);
                break;
            case STATE.CRASH:
                break;
            case STATE.DELETE:
                break;
        }
    }

    void ChangeState(STATE s)
    {
        state = s;

        switch (s)
        {
            case STATE.CREATE:
                break;
            case STATE.WORK:
                break;
            case STATE.CRASH:
                CrashEffect();
                Damage();
                ChangeState(STATE.DELETE);
                break;
            case STATE.DELETE:
                Delete();
                break;
        }
    }

    void CrashEffect()
    {
        GameObject obj = Instantiate(Obj_CrashEffect);
        obj.transform.position = EffectPos;
        obj.transform.rotation = this.transform.rotation;
    }

    void Damage()
    {
        character.DealDamage(CrashMonster.GetComponent<Character>());
        Debug.Log("타격");
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    /*
    public void Damage(Character defender)
    {
        defender.m_CurrHP -= (Random.Range(0.95f, 1.05f) * m_Attack.m_CurrentValue - defender.m_Defense.m_CurrentValue);
        if (defender.m_CurrHP <= 0)
        {
            defender.ChangeState(Character.STATE.DEAD);
            defender.StateProcess();
        }
    }
    */

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Monster")
        {
            CrashMonster = obj.gameObject;
            EffectPos = CrashMonster.transform.position;
            EffectPos.y += 1f;
            ChangeState(STATE.CRASH);
        }
    }
}
