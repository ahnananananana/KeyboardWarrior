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
    public float fDeleteTime = 0.1f; // 삭제시간
    public GameObject Obj_CrashEffect;
    public GameObject CrashMonster;

    Character m_character;

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
        obj.transform.position = CrashMonster.transform.position;
        obj.transform.rotation = this.transform.rotation;
    }

    void Damage()
    {
        m_character.DealDamage(CrashMonster.GetComponent<Character>());
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Monster")
        {
            CrashMonster = obj.gameObject;
            ChangeState(STATE.CRASH);
        }
    }
}
