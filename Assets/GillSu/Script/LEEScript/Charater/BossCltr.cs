using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCltr : Monster
{

    public LStatBar m_HPBar;
    public Transform HPBarPos;

    public static int m_Count = 0;
    public enum STATEBOSS
    {
        CREATE, IDLE, MOVE, ATTACK, SKILL
    }
    public STATEBOSS m_State = STATEBOSS.CREATE;

    SkillSys sks;

    public bool PlayState = true;
    public GameObject EffectSkill;

    Vector3 distpos;
    public Transform PlayerTr;
    [SerializeField]
    private GameObject m_SkillRange;
    
    [SerializeField]
    private Transform Bosstr;
    public Animator Bossanim = null;
    // Start is called before the first frame update
    void Start()
    {
        Bosstr = GetComponent<Transform>();
        PlayerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Bossanim = GetComponent<Animator>(); // 자기자신 컴포넌트 안의 Animator를 갸져온다.

        AttachStatBar();
        ChangeState(STATEBOSS.IDLE);
    }

    void AttachStatBar()
    {
       // m_HPBar = (Instantiate(Resources.Load("frepab/HPBar")) as GameObject).GetComponent<LStatBar>();
        if (m_HPBar != null)
        {
            m_HPBar.SetPosition(/*transform.Find("BarPosition"), 0.15f,*/HPBarPos, Color.red, Color.gray);
        }
    }

        public void ChangeState(STATEBOSS s)
    {
        if (m_State == s) return;
        m_State = s;
        switch (m_State)
        {
            case STATEBOSS.CREATE:
                break;
            case STATEBOSS.IDLE:
                idleAction();
                StartCoroutine(disstenceTest());
                break;
            case STATEBOSS.ATTACK:
              StartCoroutine(deleyEff());
                // ChangeState(STATEBOSS.IDLE);
                //  PlayState = true;
                break;
            case STATEBOSS.MOVE:
                StartCoroutine(Move());
                break;
            case STATEBOSS.SKILL:
                Bossanim.SetBool("isSkill", true);
                Bossanim.SetInteger("isAttack", 0);
                StartCoroutine(deley());
                break;
        }
    }
    IEnumerator deley()
    {
        yield return new WaitForSeconds(7.0f);
        ChangeState(STATEBOSS.IDLE);
    }

    IEnumerator Move()
    {
        bool done = false;
        while (!done)
        {
            /*    if (Vector3.Distance(PlayerTr.position, this.transform.position) > 5)
                {
                    ChangeState(STATE.IDLE);
                }*/
            Vector3 direction = PlayerTr.position - this.transform.position;
        //    distpos = direction;
            direction.y = 0f; // y축으로 기울어지는 현상 보안
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 1f);
            Bossanim.SetBool("isIdle", false);
            if (direction.magnitude > 2) //magnitude = 인자로 들어온 벡터의 길이를 반환한다.
            {
                this.transform.Translate(0, 0, 0.1f);
                Bossanim.SetBool("isWalking", true);
                Bossanim.SetInteger("isAttack", 0);
                Bossanim.SetBool("isIdle", false);
             //   Bossanim.SetBool("isSkill", false);
               
            }
            else
            {
                Bossanim.SetBool("isIdle", true);
                Bossanim.SetBool("isWalking", false);
               // Bossanim.SetBool("isSkill", false);
                ChangeState(STATEBOSS.ATTACK);
                done = true;
                break;
            }
            yield return null;
        }
    }
    public void idleAction()
    {
        Bossanim.SetBool("isIdle", true);
        Bossanim.SetInteger("isAttack", 0);
        Bossanim.SetBool("isWalking", false);
        Bossanim.SetBool("isSkill", false);
    }

    // IEnumerator IsSkillSys(float deley)
    public void AttackAnimEvent()
    {
        if (m_SkillRange == null)
        {
            m_SkillRange = Instantiate(Resources.Load("Frepab/SkillRange") as GameObject);
            m_SkillRange.transform.position = this.transform.position;
            Destroy(m_SkillRange, 5f);
        }

        if (EffectSkill == null)
        {
            EffectSkill = Instantiate(Resources.Load("frepab/skillEffect") as GameObject);
            EffectSkill.transform.position = this.transform.position;
            Destroy(EffectSkill, 10f);
        }
    }
    IEnumerator disstenceTest()
    {
        
        while (Bosstr !=null) {
            if (Vector3.Distance(PlayerTr.position, this.transform.position) < 100)
            {
                ChangeState(STATEBOSS.MOVE);
            }
            else
            {
                ChangeState(STATEBOSS.IDLE);
            }
            yield return null;
          //  yield return new WaitForSeconds(deley);
        }
    }

    void Update()
    {
       /* if (PlayState == true)
        {
            if (Vector3.Distance(PlayerTr.position, this.transform.position) < 10)
            {
                ChangeState(STATEBOSS.MOVE);
            }
        }
        else {
           
        }*/
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("파이클");
    }


    void NomalAttack()
    {
        DealDamage(PlayerTr.GetComponent<Character>(), 2f);
        m_Count += 1;
        if (m_Count == 2)
        {
            StopAllCoroutines();
            ChangeState(STATEBOSS.SKILL);
            m_Count = 0;
        }
    }

    IEnumerator deleyEff()
    {
        bool done = false;
        while (!done)
        {
            int randomskill = 1;
            // StopCoroutine(disstenceTest(0f));
            //  Vector3 direction = PlayerTr.position - this.transform.position;
            if (randomskill < 10)
            {
                Bossanim.SetBool("isIdle", false);
                Bossanim.SetBool("isWalking", false);
                Bossanim.SetInteger("isAttack", 1);
                

                
                yield return null;
            }
            else
            {
                
            }
                 /*   if (i == 4)
                    {
                    Bossanim.SetBool("isWalking", false);
                    Bossanim.SetBool("isIdle", false);
                    Bossanim.SetInteger("isAttack", 0);
                    Bossanim.SetBool("isSkill", true);
                    done = true;
                    i = 1;

                    yield return new WaitForSeconds(deley);
                    ChangeState(STATEBOSS.IDLE);*/
                
            
                    //i++;
        }
       
        /*     else
             {
                 ChangeState(STATEBOSS.IDLE);
                 done = true;
             }*/



    }
    
    }


   
    

