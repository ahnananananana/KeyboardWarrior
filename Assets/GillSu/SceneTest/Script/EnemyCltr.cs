using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCltr : Monster
{
    public Monster stat;

    public HitText t_Text = null;

    public Transform muzzle_Pos; // 이펙트의 위치 오브젝트
    public GameObject Hit_Muzzle; // 이펙트 프리펩

    public Transform[] points;
    public int nextIdx = 1; // 0은 처음 위치이기에 1번 웨이 포인트로 설정
    public float m_speed = 3f;
    public float damping = 5.0f;

    private Transform PlayerTr;
    private Transform tr;


    private Vector3 movePos;
    private bool isAttack = false;
    private Animator anim;

    private void OnDestroy()
    {
       // if (this.t_Text.gameObject != null) Destroy(this.t_Text.gameObject, 1.0f);
    }
    void OnHitTextOn9()
    {
        t_Text = (Instantiate(Resources.Load("frepab/Hit")) as GameObject).GetComponent<HitText>();
       // t_Text.GetComponentInChildren<Text>().text = ("Damage"); 
        if (t_Text != null)
        {
            t_Text.SetPosition(transform.Find("DamgeLog"), 0.15f);
        }
    }

    void Start()
    {
         tr = GetComponent<Transform>();

        PlayerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(tr.position, PlayerTr.position); // 플레이어 캐릭터와 몬스터의 거리를 확인
            if (dist <= 1.0f)
            {
                isAttack = true;
                anim.SetBool("isIdle", false);

            }
            else if (dist <= 5.0f) // 플레이어와 몬스터의 거리가 5.0보다 가까울경우
            {
                isAttack = false;
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position); //가야할 방향에서 현재 자기 자신의 위치를 연산
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            tr.Translate(Vector3.forward * Time.deltaTime * m_speed);
            movePos = PlayerTr.position; // 몬스터의 위치를 플레이어의 위치로 이동한다.
            }
            else
            {
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position); //가야할 방향에서 현재 자기 자신의 위치를 연산
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            tr.Translate(Vector3.forward * Time.deltaTime * m_speed);
            //movePos = points[nextIdx].position; // 거리가 멀 경우  else 몬스터의 위치는 다음 인덱스 포지션으로 이동.
            anim.SetBool("isIdle", false);
            movePos = points[0].position;
               if (tr.transform.position == movePos)
                {
                    anim.SetBool("isIdle", true);
                    isAttack = false;
                }
            }
            anim.SetBool("isAttack", isAttack);
            anim.SetBool("isidle", false);

        /*if (!isAttack)
            {
                Quaternion rot = Quaternion.LookRotation(movePos - tr.position); //가야할 방향에서 현재 자기 자신의 위치를 연산
                tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
                tr.Translate(Vector3.forward * Time.deltaTime * m_speed);
            }*/
    }    

    void OnTriggerEnter(Collider coll) //트리거가 웨이포인트에 충돌했을 시 발생함.
    {
        if (coll.tag == "WAY_Point")
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }

    public void OnAttack()
    {
       GameObject muzzle =  Instantiate(Hit_Muzzle, muzzle_Pos.position, Quaternion.identity);

        Destroy(muzzle, 0.5f);
        //stat.DealDamage(PlayerTr.GetComponent<Player>());
        
        OnHitTextOn9();
    }
 
}
