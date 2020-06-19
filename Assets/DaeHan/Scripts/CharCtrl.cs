using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtrl : MonoBehaviour
{
    public enum STATE // 캐릭터 상태
    {
        CREATE,IDLE,WALK,ROLL,ATTACK,DEAD
    }
    public STATE state = STATE.CREATE;

    public enum WEAPONTYPE // 사용중인 무기 종류
    {
        BOW,GUN,SWORD
    }
    public WEAPONTYPE weapontype = WEAPONTYPE.BOW;

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

    public bool bLongDistAtk = false;

    public float m_RotSpeed = 400f;
    public float m_AttackRotSpeed = 1200f;
    public float m_RollSpeed = 10.0f;

    //----------삭제할 변수----------//
    public float m_MoveSpeed = 10.0f;
    //-------------------------------//

    public LayerMask m_GroundLayer;
    public LayerMask m_MonsterLayer;

    public Camera m_MainCamera;
    
    public Animator UsingAni;
    public RuntimeAnimatorController BowAni;
    public RuntimeAnimatorController GunAni;
    public RuntimeAnimatorController SwordAni;
    public Projectile proj;

    

    private void Start()
    {
        ChangeWeapon(WEAPONTYPE.BOW, Bow_Obj);
    }

    private void Update()   
    {
        StateProcess();
    }

    protected virtual void StateProcess()
    {
        switch (state)
        {
            case STATE.CREATE:
                UsingAni = GetComponent<Animator>();
                ChangeState(STATE.IDLE);
                break;
            case STATE.IDLE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
                {
                    Picking(true);
                }
                else if (Input.GetMouseButton(0))
                {
                    Picking(false);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    ChangeState(STATE.ROLL);
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
                if (Input.GetKeyDown(KeyCode.S))
                {
                    UsingAni.SetTrigger("Hit");
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    ChangeState(STATE.DEAD);
                }
                //-------------------------------------------//
                break;
            case STATE.WALK:
                Rotating(m_RotSpeed);
                Moving(true);
                break;
            case STATE.ROLL:
                Roll();
                break;
            case STATE.ATTACK:
                Rotating(m_AttackRotSpeed);
                break;
            case STATE.DEAD:
                break;
        }
    }

    protected virtual void ChangeState(STATE s)
    {
        if (state == s) return;
        state = s;

        switch (s)
        {
            case STATE.CREATE:
                break;
            case STATE.IDLE:
                break;
            case STATE.WALK:
                ReadyMove();
                break;
            case STATE.ROLL:
                UsingAni.SetTrigger("Roll");
                break;
            case STATE.ATTACK:
                Attack();
                break;
            case STATE.DEAD:
                Dead();
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
        //Ani.Play("Idle");
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

    protected void Picking(bool LeftControl)
    {
        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, m_MonsterLayer))
        {
            Vector3 pos = transform.position;
            pos.x = hit.point.x;
            pos.z = hit.point.z;

            m_MoveData.TargetPosition = pos;

            Debug.DrawRay(ray.origin, ray.direction * 30, Color.yellow);

            if (UsingAni.GetBool("Walk") == true)
                UsingAni.SetBool("Walk", false);

            ChangeState(STATE.ATTACK);
        }

        else if (Physics.Raycast(ray, out hit, 1000f, m_GroundLayer))
        {
            Vector3 pos = transform.position;
            pos.x = hit.point.x;
            pos.z = hit.point.z;
            m_MoveData.TargetPosition = pos;

            Debug.DrawRay(ray.origin, ray.direction * 30, Color.yellow);

            switch (LeftControl)
            {
                case false:
                    ChangeState(STATE.WALK);
                    break;
                case true:
                    ChangeState(STATE.ATTACK);
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
        UsingAni.SetBool("Walk", true);

        if (bPlayer) // 이동중일 때 다른 입력을 받기 위함
        {
            if (Input.GetMouseButton(0))
            {
                ChangeState(STATE.IDLE);

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
                ChangeState(STATE.ROLL);
            }
        }

        float delta = m_MoveSpeed * Time.smoothDeltaTime;
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
            ChangeState(STATE.IDLE);
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
        if (obj.tag == "AtkBox" && state == STATE.IDLE)
        {
            UsingAni.SetTrigger("Hit"); // 상체 애니메이션만 해야함
        }
    }
}

//this.transform.LookAt(V1,V2) == this.transform.LookAt(바라볼목표, 자신의 업벡터)
//Invoke("Restore", 0.1f); == 0.1초 뒤에 Restore라는 함수 호출 
//GameObject.SendMessage == 해당 게임 오브젝트에 있는 특정 함수를 호출하기 위해 사용
//DX11 랜더링은 버텍스 쉐이더와 픽셀 쉐이더가 한 쌍을 이루어야 작동함