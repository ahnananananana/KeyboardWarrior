
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum STATE
    {
        CREATE, MOVE, CRASH, OUT
    }

    public STATE state = STATE.CREATE;

    public GameObject Obj_CrashEffect;
    public GameObject Obj_GunFireEffect;
    public GameObject CrashMonster;
    public LayerMask Monster;

    public float fDeleteProjTime = 2.0f;
    public float fMoveSpeed = 30.0f;
    public float fTime = 0.0f;
    Vector3 EffectPos;
    Character m_character;////

    Vector3 V3_Dir;

    public Character character { get => m_character; set => m_character = value; }

    private void Update()
    {
        StateProcess();
    }

    void StateProcess()
    {
        switch (state)
        {
            case STATE.CREATE:
                break;
            case STATE.MOVE:
                MoveToTarget();
                break;
            case STATE.CRASH:
                break;
            case STATE.OUT:
                break;
        }
    }

    public void ChangeState(STATE s)
    {
        if (state == s) return;

        state = s;

        switch (state)
        {
            case STATE.CREATE:
                break;
            case STATE.MOVE:
                if (Obj_GunFireEffect != null)
                {
                    GameObject obj = Instantiate(Obj_GunFireEffect);
                    obj.transform.position = this.transform.position;
                    obj.transform.rotation = this.transform.rotation;
                }
                break;
            case STATE.CRASH:
                CreateEffect();
                Damage();
                ChangeState(STATE.OUT);
                break;
            case STATE.OUT:
                Out();
                break;
        }
    }

    public void MoveToTarget()
    {
        Vector3 pos = transform.localPosition;
        float delta = fMoveSpeed * Time.smoothDeltaTime;
        Vector3 target = pos + V3_Dir * delta;

        Ray ray = new Ray();
        ray.origin = pos;
        ray.direction = target - pos;
        ray.direction.Normalize();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, delta, Monster)) // 충돌이 났을 때
        {
            CrashMonster = hit.transform.gameObject; // == 충돌한 개체
            EffectPos = CrashMonster.transform.position;
            EffectPos.y += 1f;
            ChangeState(STATE.CRASH);
        }
        else
        {
            transform.position = target;

            fTime += Time.smoothDeltaTime;
            if (fTime >= fDeleteProjTime)
            {
                ChangeState(STATE.OUT);
            }
        }
    }

    void CreateEffect()
    {
        GameObject obj = Instantiate(Obj_CrashEffect);
        obj.transform.position = EffectPos;
        obj.transform.rotation = this.transform.rotation;
    }

    void Out()
    {
        Destroy(gameObject);
    }

    void Damage()
    {
        character.DealDamage(CrashMonster.GetComponent<Character>());
    }

    public void OnFire(Vector3 dir)
    {
        V3_Dir = dir;
        ChangeState(STATE.MOVE);
    }
}