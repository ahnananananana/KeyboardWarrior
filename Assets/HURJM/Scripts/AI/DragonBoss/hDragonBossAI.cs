using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private hFlame m_Flame;

    protected override void Update()
    {
        m_RandomVal = Random.Range(0, 100);
        m_PosAcc = 0;
        base.Update();
    }

    private void Start()
    {
        m_Flame.contact += FlameContact;
    }

    protected override void InitBTS()
    {
        hSelectorNode subRoot = new hSelectorNode(new List<hBTSNode>());
        m_RootNode.children.Add(subRoot);


        hDecorationNode isEncounter = new hDecorationNode(IsEncounter);

        hSequenceNode encounter = new hSequenceNode();
        isEncounter.child = encounter;

        hActionNode scream = new hDoAnimation(this, "Scream", m_Target.transform);

        hDoAction setMet = new hDoAction(() => { m_IsMet = true; return NodeState.SUCCESS; });

        encounter.children.Add(scream);
        encounter.children.Add(setMet);


        hDecorationNode isPattern1 = new hDecorationNode(false);
        isPattern1.condition = () => { if (m_Character.CurrHP > m_Character.m_MaxHP.m_CurrentValue / 2) return true; return false; };
        hSelectorNode pattern1 = new hSelectorNode();
        isPattern1.child = pattern1;

        hDecorationNode isClawAttack = new hDecorationNode(false);
        isClawAttack.condition = () => { m_PosAcc += m_Pattern1Pos[0]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode clawAttack = new hSequenceNode();
        hSearchAndFollowAction searchAndFollow = new hSearchAndFollowAction(this, m_Target.transform, null, m_AttackRange);
        hActionNode clawAttackAni = new hDoAnimation(this, "Claw Attack");

        isClawAttack.child = clawAttack;
        clawAttack.children.Add(searchAndFollow);
        clawAttack.children.Add(clawAttackAni);


        hDecorationNode isBiteAttack = new hDecorationNode(false);
        isBiteAttack.condition = () => { m_PosAcc += m_Pattern1Pos[1]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode biteAttack = new hSequenceNode();
        hSearchAndFollowAction searchAndFollow2 = new hSearchAndFollowAction(this, m_Target.transform, null, m_AttackRange);
        hActionNode biteAttackAni = new hDoAnimation(this, "Bite Attack");

        isBiteAttack.child = biteAttack;
        biteAttack.children.Add(searchAndFollow2);
        biteAttack.children.Add(biteAttackAni);


        hDecorationNode isFlameAttack = new hDecorationNode(false);
        isFlameAttack.condition = () => { m_PosAcc += m_Pattern1Pos[2]; if (m_RandomVal < m_PosAcc) return true; return false; };
        hSequenceNode flameAttack = new hSequenceNode();
        hActionNode flameAttackAnim = new hDoAnimation(this, "Flame Attack", m_Target.transform);

        isFlameAttack.child = flameAttack;
        flameAttack.children.Add(flameAttackAnim);


        pattern1.children.Add(isClawAttack);
        pattern1.children.Add(isBiteAttack);
        pattern1.children.Add(isFlameAttack);

        subRoot.children.Add(isEncounter);
        subRoot.children.Add(isPattern1);
    }

    private bool IsEncounter()
    {
        if (m_IsMet)
            return false;

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

    public void FlameContact(Collider other)
    {

    }

}
