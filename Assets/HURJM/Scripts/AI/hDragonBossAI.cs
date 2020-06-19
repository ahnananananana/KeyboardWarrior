using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDragonBossAI : hMonsterAI
{
    public GameObject target;
    public bool m_IsMet;
    [SerializeField]
    private Transform m_FlamePos;

    protected override void InitBTS()
    {
        hSelectorNode subRoot = new hSelectorNode(new List<hBTSNode>());
        m_RootNode.children.Add(subRoot);


        hDecorationNode isEncounter = new hDecorationNode(IsEncounter);

        hSequenceNode encounter = new hSequenceNode();
        isEncounter.child = encounter;

        hActionNode scream = new hDoAnimation(this, "Scream", target.transform);

        hDoAction setMet = new hDoAction(() => { m_IsMet = true; return NodeState.SUCCESS; });

        encounter.children.Add(scream);
        encounter.children.Add(setMet);


        hDecorationNode isDead = new hDecorationNode(IsDead);
        hActionNode dead = new hDoAnimation(this, "Die");
        isDead.child = dead;


        hSequenceNode pattern1 = new hSequenceNode();

        hSearchAndFollowAction searchAndFollow = new hSearchAndFollowAction(this, m_Target.transform, null, m_AttackRange);
        hActionNode clawAttack = new hDoAnimation(this, "Claw Attack");

        pattern1.children.Add(searchAndFollow);
        pattern1.children.Add(clawAttack);


        subRoot.children.Add(isEncounter);
        subRoot.children.Add(isDead);
        subRoot.children.Add(pattern1);
    }

    private bool IsEncounter()
    {
        if (m_IsMet)
            return false;

        return true;
    }

    private bool IsDead()
    {
        if (character.m_state == Character.STATE.DEAD)
            return true;
        return false;
    }

    protected override void Dead()
    {
        throw new System.NotImplementedException();
    }

}
