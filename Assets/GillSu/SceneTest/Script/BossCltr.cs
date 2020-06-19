using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCltr : MonoBehaviour
{
    public enum STATE
    {
        CREATE, IDLE, MOVE, ATTACK
    }
    public STATE m_State = STATE.CREATE;

    public float m_MoveSpeed = 3f;
    public float m_TunSpdde = 5f;
    private bool Play = true;

    private Transform tr;
    public Transform PlayerTr;

    //보스 스폰 위치
    private Vector3 SponPos = Vector3.zero;

    private Vector3 movePos;
    private bool BossAttack = false;
    public Animator Bossanim = null;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        PlayerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Bossanim = GetComponent<Animator>();
        ChangeState(STATE.IDLE);
    }
    public void ChangeState(STATE s)
    {
        if (m_State == s) return;
        m_State = s;
        switch (m_State)
        {
            case STATE.CREATE:
                break;
            case STATE.IDLE:
                Attack();
                break;
            case STATE.ATTACK:
                StartCoroutine(AttackRandom(STATE.IDLE));

                break;
            case STATE.MOVE:
                break;
        }
    }
    void Attack()
    {
        float dist = Vector3.Distance(tr.position, PlayerTr.position); // 플레이어 캐릭터와 몬스터의 거리를 확인


        if (dist <= 1.0f)
        {
            ChangeState(STATE.ATTACK);
            //BossAttack = false;
        }
        /*else if (dist <= 5.0f) // 플레이어와 몬스터의 거리가 5.0보다 가까울경우
        {
            BossAttack = false;
            movePos = PlayerTr.position; // 몬스터의 위치를 플레이어의 위치로 이동한다.

        }
        else
        {
            movePos = Vector3.zero;
            BossAttack = false;
        }
        Bossanim.SetBool("BossAttack", BossAttack); //기본공격

        if (!BossAttack)
        {
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position); //가야할 방향에서 현재 자기 자신의 위치를 연산
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * m_TunSpdde);
            tr.Translate(Vector3.forward * Time.deltaTime * m_MoveSpeed);
        }*/
    }
        // Update is called once per frame
        void Update()
        {

        }
    IEnumerator AttackRandom(STATE s)
    {
        yield return null;
        while (Play)
        {
            int a = Random.Range(0, 3);
            switch (a)
            {
                case 0:
                    BossAttack = true;
                    Bossanim.SetBool("BossAttack", BossAttack);

                    break;
                case 1:
                    BossAttack = true;
                    Bossanim.SetBool("BossAttack2", BossAttack);
                    break;
                case 2:
                    BossAttack = true;
                    Bossanim.SetBool("BossAttack3", BossAttack);
                    break;
            }
            BossAttack = false;
            ChangeState(s);
        }
    }
    }

