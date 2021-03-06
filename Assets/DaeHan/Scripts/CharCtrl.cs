﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtrl : Character
{
    public enum CTRLSTATE // 캐릭터 상태
    {
        CREATE,IDLE,WALK,ROLL,ATTACK,DEAD
    }
    public CTRLSTATE ctrlstate = CTRLSTATE.CREATE;

    public enum WEAPONTYPE // 사용중인 무기 종류
    {
        BOW,GUN,SWORD
    }
    public WEAPONTYPE weapontype = WEAPONTYPE.BOW;

    public enum LAYERSTATE
    {
        PLAYER, IMMORTAL
    }
    LAYERSTATE layerstate = LAYERSTATE.PLAYER;

    protected struct MoveData
    {
        public Vector3 TargetPosition;
        public Vector3 CurrentPosition;
        public Vector3 CurrentRot;
        public Vector3 MoveDir;
        public float MoveDist;
        public float RotY;
    }
    protected MoveData m_MoveData;

    public GameObject Weapon_Obj;
    public GameObject Bow_Obj;
    public GameObject Gun_Obj;
    public GameObject Sword_Obj;
    public GameObject Arrow_Obj;
    public GameObject Bullet_Obj;
    public GameObject SwordAtkBox;

    public Transform UsingWeaponTR;
    public Transform BowTR;
    public Transform GunTR;
    public Transform SwordTR;
    public Transform ArrowMuzzleTR;
    public Transform BulletMuzzleTR;
    public Transform SwordAtkBoxTR;

    Vector3 MousePos = Vector3.zero;

    public GameObject TargettingMonster;

    public bool isAttacking = false;
    public bool isMoving = false;

    public bool bLongDistAtk = false;

    public float m_RotSpeed = 400f;
    public float m_AttackRotSpeed = 1200f;
    public float m_RollSpeed = 10.0f;

    public LayerMask m_GroundLayer;
    public LayerMask m_MonsterLayer;

    public Camera m_MainCamera;
    
    public Animator UsingAni;
    public RuntimeAnimatorController BowAni;
    public RuntimeAnimatorController GunAni;
    public RuntimeAnimatorController SwordAni;

    public Projectile proj;
    public Player player;
    public Character character;

    private void Awake()
    {
        UsingWeaponTR = BowTR;
    }
    private void Start()
    {
        UsingAni = GetComponent<Animator>();
        ChangeWeapon(WEAPONTYPE.BOW, Bow_Obj);
    }

    private void Update()   
    {
        CtrlStateProcess();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("파이클");
    }


    protected virtual void CtrlStateProcess()
    {
        switch (ctrlstate)
        {
            case CTRLSTATE.CREATE:
                ChangeCtrlState(CTRLSTATE.IDLE);
                break;
            case CTRLSTATE.IDLE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
                {
                    Picking(true);
                }
                else if (Input.GetMouseButton(0))
                {
                    Picking(false);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isAttacking = false;
                    isMoving = false;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    ChangeCtrlState(CTRLSTATE.ROLL);
                }

                //------------------지울거-------------------//
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    ChangeWeapon(WEAPONTYPE.BOW, Bow_Obj);
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    ChangeWeapon(WEAPONTYPE.GUN, Gun_Obj);
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    ChangeWeapon(WEAPONTYPE.SWORD, Sword_Obj);
                }
                //-------------------------------------------//
                break;
            case CTRLSTATE.WALK:
                if (Input.GetMouseButtonUp(0))
                {
                    isMoving = false;
                }
                Rotating(m_RotSpeed);
                Moving(true);
                break;
            case CTRLSTATE.ROLL:
                if (Input.GetMouseButtonUp(0))
                {
                    isMoving = false;
                }
                Roll();
                break;
            case CTRLSTATE.ATTACK:
                if (Input.GetMouseButtonUp(0))
                {
                    isAttacking = false;
                }
                Rotating(m_AttackRotSpeed);
                break;
            case CTRLSTATE.DEAD:
                break;
        }
    }

    protected virtual void ChangeCtrlState(CTRLSTATE s)
    {
        if (ctrlstate == s) return;
        ctrlstate = s;

        switch (s)
        {
            case CTRLSTATE.CREATE:
                break;
            case CTRLSTATE.IDLE:
                break;
            case CTRLSTATE.WALK:
                ReadyMove();
                break;
            case CTRLSTATE.ROLL:
                MousePicking();
                this.transform.LookAt(MousePos, this.transform.up);
                UsingAni.SetTrigger("Roll");
                ChangeLayer(LAYERSTATE.IMMORTAL);
                break;
            case CTRLSTATE.ATTACK:
                Attack();
                break;
            case CTRLSTATE.DEAD:
                Dead();
                break;
        }
    }

    void ChangeLayer(LAYERSTATE s)
    {
        layerstate = s;

        switch (s)
        {
            case LAYERSTATE.PLAYER:
                this.gameObject.layer = LayerMask.NameToLayer("Player");
                break;
            case LAYERSTATE.IMMORTAL:
                this.gameObject.layer = LayerMask.NameToLayer("Immortal");
                break;
        }
    }

    protected void ChangeWeapon(WEAPONTYPE w, GameObject weapon) // 무기 장착 함수
    {
        if (Weapon_Obj != null) Destroy(Weapon_Obj);

        switch (w)
        {
            case WEAPONTYPE.BOW:
                bLongDistAtk = true;
                UsingWeaponTR = BowTR;
                UsingAni.runtimeAnimatorController = BowAni;
                break;
            case WEAPONTYPE.GUN:
                bLongDistAtk = true;
                UsingWeaponTR = GunTR;
                UsingAni.runtimeAnimatorController = GunAni;
                break;
            case WEAPONTYPE.SWORD:
                bLongDistAtk = false;
                UsingWeaponTR = SwordTR;
                UsingAni.runtimeAnimatorController = SwordAni;
                break;
        }
        Weapon_Obj = Instantiate(weapon) as GameObject;
        Weapon_Obj.transform.SetParent(UsingWeaponTR);
        Weapon_Obj.transform.position = UsingWeaponTR.position;
        Weapon_Obj.transform.rotation = UsingWeaponTR.rotation;
    }

    protected void Roll()
    {
        Vector3 pos = transform.localPosition;
        float delta = m_RollSpeed * Time.smoothDeltaTime;
        Vector3 target = pos + this.transform.forward * delta;

        transform.localPosition = target;
    }

    protected void Attack()
    {
        ReadyMove();
        UsingAni.SetFloat("AttackSpeed", player.m_AttackSpeed.m_CurrentValue);
        UsingAni.SetTrigger("Attack");
    }

    protected void BowFire()
    {
        GameObject obj = Instantiate(Arrow_Obj) as GameObject;
        proj = obj.GetComponent<Projectile>();
        obj.transform.position = ArrowMuzzleTR.position;
        obj.transform.rotation = this.transform.rotation;
        proj.character = GetComponent<Player>();
        proj.OnFire(ArrowMuzzleTR.forward);
    }

    protected void GunFire()
    {
        GameObject obj = Instantiate(Bullet_Obj) as GameObject;
        proj = obj.GetComponent<Projectile>();
        obj.transform.position = BulletMuzzleTR.position;
        obj.transform.rotation = this.transform.rotation;
        proj.character = GetComponent<Player>();
        proj.OnFire(BulletMuzzleTR.forward);
    }

    protected void SwordAttack()
    {
        GameObject obj = Instantiate(SwordAtkBox);
        obj.transform.position = SwordAtkBoxTR.transform.position;
        obj.transform.rotation = SwordAtkBoxTR.transform.rotation;
    }

    protected void Dead()
    {
        StopAllCoroutines();
        UsingAni.Play("Death");
    }

    protected void MousePicking()
    {
        if (m_MainCamera == null) m_MainCamera = Camera.main;
        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000.0f, m_GroundLayer))
        {
            MousePos.x = hit.point.x;
            MousePos.z = hit.point.z;
        }
    }

    protected void Picking(bool LeftControl) // 캐릭터 이동&공격시 클릭을 하면 호출할 함수
    {
        if (m_MainCamera == null) m_MainCamera = Camera.main;

        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (LeftControl == false && isAttacking == false && isMoving == false && Physics.Raycast(ray, out hit, 1000.0f, m_MonsterLayer)) // 타겟팅 공격 시작시 1회 호출되어 몬스터의 정보를 담기 위함
        {
            isAttacking = true;

            TargettingMonster = hit.transform.gameObject;
            m_MoveData.TargetPosition = TargettingMonster.transform.position;
            ReadyMove();

            if (bLongDistAtk == false && m_MoveData.MoveDist > 4f)
            {
                ChangeCtrlState(CTRLSTATE.WALK);
            }
            else
            {
                if (UsingAni.GetBool("Walk") == true)
                    UsingAni.SetBool("Walk", false);
                ChangeCtrlState(CTRLSTATE.ATTACK);
            }
        }
        else if (LeftControl == false && isAttacking == true && isMoving == false && Physics.Raycast(ray, out hit, 1000.0f)) // 타겟팅 공격시 마우스를 쭉 누르고 있을 떄
        {
            m_MoveData.TargetPosition = TargettingMonster.transform.position;
            ReadyMove();

            if (bLongDistAtk == false && m_MoveData.MoveDist > 4f /*사정거리*/)
            {
                ChangeCtrlState(CTRLSTATE.WALK);
            }
            else
            {
                if (UsingAni.GetBool("Walk") == true)
                    UsingAni.SetBool("Walk", false);
                ChangeCtrlState(CTRLSTATE.ATTACK);
            }
        }
        else if (Physics.Raycast(ray, out hit, 1000.0f, m_GroundLayer))
        {
            Vector3 pos = transform.position;
            pos.x = hit.point.x;
            pos.z = hit.point.z;
            m_MoveData.TargetPosition = pos;

            switch (LeftControl)
            {
                case false:
                    isMoving = true;
                    ChangeCtrlState(CTRLSTATE.WALK);
                    break;
                case true:
                    isAttacking = true;
                    ChangeCtrlState(CTRLSTATE.ATTACK);
                    break;
            }
        }
    }

    protected void ReadyMove()
    {
        m_MoveData.MoveDir = m_MoveData.TargetPosition - transform.position;
        m_MoveData.MoveDir.Normalize();
        m_MoveData.MoveDist = Vector3.Distance(transform.position, m_MoveData.TargetPosition);
        m_MoveData.CurrentPosition = transform.position;

        m_MoveData.RotY = Mathf.Acos(Vector3.Dot(transform.forward, m_MoveData.MoveDir)) * (180.0f / Mathf.PI);

        if (Vector3.Dot(transform.right, m_MoveData.MoveDir) < 0f)
        {
            m_MoveData.RotY = -m_MoveData.RotY;
        }

        m_MoveData.CurrentRot = transform.localRotation.eulerAngles;
    }

    protected void Moving(bool bPlayer)
    {
        if (UsingAni.GetBool("Walk") == false)
        {
            UsingAni.SetFloat("WalkSpeed", player.m_MoveSpeed.m_CurrentValue);
            UsingAni.SetBool("Walk", true);
        }

        if (bPlayer) // 이동중일 때 다른 입력을 받기 위함
        {
            if (Input.GetMouseButton(0))
            {
                ChangeCtrlState(CTRLSTATE.IDLE);

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    UsingAni.SetBool("Walk", false);
                    Picking(true);
                }
                else
                    Picking(false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                UsingAni.SetBool("Walk", false);
                UsingAni.Play("Idle"); // 구르기 시전 딜레이때문에 넣음
                ChangeCtrlState(CTRLSTATE.ROLL);
            }
        }

        float delta = character.m_MoveSpeed.m_BaseValue * Time.smoothDeltaTime;
        if (m_MoveData.MoveDist - delta < 0.0f)
        {
            delta = m_MoveData.MoveDist;
        }
        m_MoveData.MoveDist -= delta;

        m_MoveData.CurrentPosition += m_MoveData.MoveDir * delta;

        transform.position = m_MoveData.CurrentPosition;

        if (m_MoveData.MoveDist < 0.01f)
        {
            UsingAni.SetBool("Walk", false);
            ChangeCtrlState(CTRLSTATE.IDLE);
        }
    }

    protected void Rotating(float KindOfRotate)
    {
        float delta = KindOfRotate * Time.smoothDeltaTime;

        if (m_MoveData.RotY > 0f)
        {
            if (m_MoveData.RotY - delta < 0.0f)
            {
                delta = m_MoveData.RotY;
            }
            m_MoveData.RotY -= delta;
        }
        else if (m_MoveData.RotY < 0f)
        {
            if (m_MoveData.RotY + delta > 0.0f)
            {
                delta = -m_MoveData.RotY;
            }
            m_MoveData.RotY += delta;
            delta = -delta;
        }
        else
        {
            delta = 0f;
        }

        this.transform.Rotate(Vector3.up, delta);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "AtkBox" && ctrlstate == CTRLSTATE.IDLE)
        {
            UsingAni.SetTrigger("Hit"); // 상체 애니메이션만 해야함
        }
    }
}

//this.transform.LookAt(V1,V2) == this.transform.LookAt(바라볼목표, 자신의 업벡터)
//Invoke("Restore", 0.1f); == 0.1초 뒤에 Restore라는 함수 호출 
//GameObject.SendMessage == 해당 게임 오브젝트에 있는 특정 함수를 호출하기 위해 사용
//DX11 랜더링은 버텍스 쉐이더와 픽셀 쉐이더가 한 쌍을 이루어야 작동함