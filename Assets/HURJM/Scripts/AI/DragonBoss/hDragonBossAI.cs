using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class hDragonBossAI : hMonsterAI
{
    public bool m_IsMet;
    [SerializeField]
    private Transform m_FlamePos;
    private int m_RandomVal;
    [SerializeField]
    private int[] m_Pattern1Pos, m_Pattern2Pos;
    private int m_PosAcc;

    [SerializeField]
    private float m_ClawAttackRange, m_BiteAttackRange, m_ClawDamage, m_BiteDamage;

    [SerializeField]
    private hFlame m_Flame;
    [SerializeField]
    private int m_FlameDamage;
    [SerializeField]
    private float m_FlameDamageSpeed;
    private float m_FlameLastDamageTime;

    [SerializeField]
    private Slider m_HealthBar;

    [SerializeField]
    private float m_Pattern2Interval;
    private float m_LastPattern2Time;
    [SerializeField]
    private hFlameBomb m_FlameBombPrefab;
    private List<hFlameBomb> m_FlameBombList;
    [SerializeField]
    private int m_FlameBombNum;
    [SerializeField]
    private float m_FlameBombDamage;
    private hMap m_Map;
    private BoxCollider m_Boundary;
    [SerializeField]
    private PlayableDirector m_Intro;
    [SerializeField]
    private float m_EncounterDis;

    protected override void Awake()
    {
        base.Awake();
        m_FlameLastDamageTime = Time.time - m_FlameDamageSpeed;
        m_Flame.contact += FlameContact;
        m_HealthBar.maxValue = m_Character.m_MaxHP.m_CurrentValue;
        m_HealthBar.value = m_Character.m_MaxHP.m_CurrentValue;
        m_Character.changeEvent += RefreshHealthBar;

        m_HealthBar.gameObject.SetActive(false);

        m_LastPattern2Time = -m_Pattern2Interval;
        m_FlameBombList = new List<hFlameBomb>();

        var ta = m_Intro.playableAsset as TimelineAsset;
        var temp = ta.GetOutputTracks();
        var brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        foreach(var kvp in temp)
        {
            if(kvp is CinemachineTrack)
            {
                m_Intro.SetGenericBinding(kvp, brain);
                break;
            }
        }
        m_Map = FindObjectOfType<hMap>();
        m_Boundary = m_Map.boundary as BoxCollider;
    }

    protected override void Update()
    {
        m_RandomVal = Random.Range(0, 100);
        m_PosAcc = 0;
        base.Update();
    }

    protected override void InitBTS()
    {
        hDecorationNode isEncounter = new hDecorationNode(()=> { if (Vector3.Distance(m_Target.transform.position, transform.position) < m_EncounterDis) return true; return false; }, false);
        m_RootNode.children.Add(isEncounter);


        hSelectorNode subRoot = new hSelectorNode(new List<hBTSNode>());
        isEncounter.child = subRoot;


        hDecorationNode isEncounterScream = new hDecorationNode(IsEncounter, false);
        hSequenceNode encounter = new hSequenceNode();
        isEncounterScream.child = encounter;

        hActionNode setMet = new hDoAction(() => { m_IsMet = true; m_Intro.Play(); return NodeState.SUCCESS; });
        hActionNode scream = new hDoAnimation(this, "Scream", m_Target.transform);

        encounter.children.Add(setMet);
        encounter.children.Add(scream);


        hDecorationNode isWin = new hDecorationNode(() => { if (m_Target.m_state == Character.STATE.DEAD) return true; return false; }, false);
        isWin.child = scream;

/*
        hDecorationNode isPattern1 = new hDecorationNode(false);
        isPattern1.condition = () => { if (m_Character.CurrHP > m_Character.m_MaxHP.m_CurrentValue / 2) return true; return false; };*/
        hSelectorNode pattern1 = new hSelectorNode();
        //isPattern1.child = pattern1;

        hDecorationNode isClawAttack = new hDecorationNode(false);
        isClawAttack.condition = () => { m_PosAcc += m_Pattern1Pos[0]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode clawAttack = new hSequenceNode();
        hSearchAndFollowAction searchAndFollow = new hSearchAndFollowAction(this, m_Target.transform, ref m_ClawAttackRange);
        hActionNode clawAttackAni = new hDoAnimation(this, "Claw Attack");

        isClawAttack.child = clawAttack;
        clawAttack.children.Add(searchAndFollow);
        clawAttack.children.Add(clawAttackAni);


        hDecorationNode isBiteAttack = new hDecorationNode(false);
        isBiteAttack.condition = () => { m_PosAcc += m_Pattern1Pos[1]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode biteAttack = new hSequenceNode();
        hSearchAndFollowAction searchAndFollow2 = new hSearchAndFollowAction(this, m_Target.transform, ref m_BiteAttackRange);
        hActionNode biteAttackAni = new hDoAnimation(this, "Bite Attack");

        isBiteAttack.child = biteAttack;
        biteAttack.children.Add(searchAndFollow2);
        biteAttack.children.Add(biteAttackAni);


        hDecorationNode isFlameAttack = new hDecorationNode(false);
        isFlameAttack.condition = () => { m_PosAcc += m_Pattern1Pos[2]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode flameAttack = new hSequenceNode();
        hActionNode faceToTarget = new hDoAction(() => { transform.LookAt(m_Target.transform); return NodeState.SUCCESS; });
        hActionNode flameAttackAnim = new hDoAnimation(this, "Flame Attack", m_Target.transform);

        isFlameAttack.child = flameAttack;
        flameAttack.children.Add(faceToTarget);
        flameAttack.children.Add(flameAttackAnim);


        pattern1.children.Add(isClawAttack);
        pattern1.children.Add(isBiteAttack);
        pattern1.children.Add(isFlameAttack);



        hDecorationNode isPattern2 = new hDecorationNode(false);
        isPattern2.condition = IsPattern2;
        hSequenceNode pattern2 = new hSequenceNode();
        isPattern2.child = pattern2;

        hActionNode offCollider = new hDoAction(() => { m_Collider.enabled = false; return NodeState.SUCCESS; });
        hActionNode takeOff = new hDoAnimation(this, "Take Off", false);
        hActionNode goUp = new hDoAction(() => { return MoveVertical(3f, 20f); });
        hActionNode castFlameBomb = new hDoAction(CastFlameBomb);
        hActionNode isEnd = new hDoAction(()=> { if (m_FlameBombList.Count == 0) return NodeState.SUCCESS; return NodeState.RUNNING; });
        hActionNode goDown = new hDoAction(() => { return MoveVertical(3f, 0); });
        hActionNode land = new hDoAnimation(this, "Land", false);
        hActionNode resetCoolTime = new hDoAction(() => { m_LastPattern2Time = Time.time; m_Collider.enabled = true; return NodeState.SUCCESS; });


        pattern2.children.Add(offCollider);
        pattern2.children.Add(takeOff);
        pattern2.children.Add(goUp);
        pattern2.children.Add(castFlameBomb);
        pattern2.children.Add(isEnd);
        pattern2.children.Add(goDown);
        pattern2.children.Add(land);
        pattern2.children.Add(resetCoolTime);


        subRoot.children.Add(isEncounterScream);
        subRoot.children.Add(isWin);
        subRoot.children.Add(isPattern2);
        subRoot.children.Add(pattern1);
    }

    private NodeState MoveVertical(float inSpeed, float inHeight)
    {
        m_Collider.enabled = false;
        float speed = inSpeed;
        float distance = m_Root.position.y - inHeight;
        if (Mathf.Abs(distance) > 0.1f)
        {
            Vector3 newPos = m_Root.position;
            float direction;
            if (distance > 0) direction = -1f;
            else direction = 1f;
            newPos.y += direction * speed * Time.smoothDeltaTime;
            m_Root.position = newPos;
            return NodeState.RUNNING;
        }
        return NodeState.SUCCESS;
    }

    private void FlameBombDamage(Collider inTarget)
    {
        if (inTarget.gameObject != m_Target.gameObject) return;

        if (m_FlameLastDamageTime + m_FlameDamageSpeed < Time.time)
        {
            m_FlameLastDamageTime = Time.time;
            m_Character.DealDamage(m_Target, m_FlameBombDamage);
        }
    }

    private NodeState CastFlameBomb()
    {
        for (int i = 0; i < m_FlameBombNum; ++i)
        {
            hFlameBomb flameBomb = Instantiate(m_FlameBombPrefab);

            Vector3 pos =
                new Vector3(
                    Random.Range(m_Boundary.bounds.min.x, m_Boundary.bounds.max.x), 
                    0f, 
                    Random.Range(m_Boundary.bounds.min.z, m_Boundary.bounds.max.z)
                    );
            flameBomb.SetPos(pos);
            flameBomb.character = m_Character;
            flameBomb.triggerEvent += FlameBombDamage;
            flameBomb.endEvent += (hFlameBomb inBomb) => { m_FlameBombList.Remove(inBomb); DestroyImmediate(inBomb.gameObject); };
            m_FlameBombList.Add(flameBomb);
        }
        return NodeState.SUCCESS;
    }

    private bool IsPattern2()
    {
        if (m_Character.CurrHP < m_Character.m_MaxHP.m_CurrentValue / 2 && m_LastPattern2Time + m_Pattern2Interval < Time.time)
        {
            Debug.Log("Pattern2");
            return true;
        }

        return false;
    }



    private bool IsEncounter()
    {
        if (m_IsMet)
            return false;

        m_HealthBar.gameObject.SetActive(true);
        return true;
    }

    protected override void Dead()
    {
        m_Animator.SetTrigger("Die");
    }

    public void StartFlameAttack()
    {
        m_Flame.OnSet(true);
    }

    public void EndFlameAttack()
    {
        m_Flame.OnSet(false);
    }

    private void FlameContact(Collider other)
    {
        if (other.gameObject != m_Target.gameObject) return;

        if (m_FlameLastDamageTime + m_FlameDamageSpeed < Time.time)
        {
            m_FlameLastDamageTime = Time.time;
            m_Character.DealDamage(m_Target, m_FlameDamage);
        }
    }

    private void RefreshHealthBar()
    {
        m_HealthBar.value = m_Character.CurrHP;
    }

    public void ClawDamage()
    {
        if (Mathf.Abs(Vector3.Distance(m_Target.transform.position, transform.position) - m_ClawAttackRange) < .5f)
            m_Character.DealDamage(m_Target, m_ClawDamage);
    }

    public void BiteDamage()
    {
        if (Mathf.Abs(Vector3.Distance(m_Target.transform.position, transform.position) - m_BiteAttackRange) < .5f)
            m_Character.DealDamage(m_Target, m_BiteDamage);
    }
}
