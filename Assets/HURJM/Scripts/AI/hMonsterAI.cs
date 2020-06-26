using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public abstract class hMonsterAI : MonoBehaviour
{
    [SerializeField]
    protected Transform m_Root;
    [SerializeField]
    protected Character m_Character;
    protected hSelectorNode m_RootNode;
    [SerializeField]
    protected Animator m_Animator;
    [SerializeField]
    protected float m_AttackRange;
    [SerializeField]
    protected Rigidbody m_RigidBody;
    [SerializeField]
    protected LayerMask m_GroundLayer;
    [SerializeField]
    protected NavMeshAgent m_Agent;

    [SerializeField]
    private Character target;
    protected Character m_Target
    {
        get
        {
            if (target == null) target = FindObjectOfType<Player>();
            return target;
        }
        set => target = value;
    }
    protected Vector3 m_Des;
    public bool m_launch;
    [SerializeField]
    protected Collider m_Collider;
    public float attackRange { get => m_AttackRange; set => m_AttackRange = value; }
    public Transform root { get => m_Root; set => m_Root = value; }
    public Character character { get => m_Character; set => m_Character = value; }
    public Animator animator { get => m_Animator; set => m_Animator = value; }
    public Rigidbody rigidBody { get => m_RigidBody; set => m_RigidBody = value; }
    public NavMeshAgent agent { get => m_Agent; set => m_Agent = value; }
    public bool launch { get => m_launch; set => m_launch = value; }
    public Collider myCollider { get => m_Collider; set => m_Collider = value; }

    protected virtual void Awake()
    {
        m_RootNode = new hSelectorNode();
        if(m_Collider == null)
            m_Collider = GetComponent<Collider>();
        m_Character.deadEvent += Dead;
    }

    protected virtual void OnEnable()
    {
        //InitBTS();
    }

    protected virtual void Update()
    {
        if(m_RootNode.children.Count == 0)
            InitBTS();
        if (m_launch)
            m_RootNode.Evaluate();
    }

    protected abstract void InitBTS();
    protected abstract void Dead();

    #region Legacy
    /*
    private enum Pattern
    {
        NONE,
        PATTERN1,
        PATTERN2,
    }
    
    [SerializeField]
    private Transform[] m_WayPoints;
    private Pattern m_CurPattern;
    private void Start()
    {
        SetPattern(Pattern.PATTERN1);
    }

    int index = 0;

    private void SetPattern(Pattern inPattern)
    {
        m_CurPattern = inPattern;
        ChangeState(STATE.IDLE);
    }

    protected override void ChangeState(STATE s)
    {
        if (state == s) return;
        state = s;

        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.WALK:
                ReadyMove();
                break;
        }
    }


    protected override void StateProcess()
    {
        PatternProcess();
        Rotating();
    }

    private void PatternProcess()
    {
        switch(m_CurPattern)
        {
            case Pattern.PATTERN1:
                Pattern1();
                break;
            case Pattern.PATTERN2:
                break;
        }
    }

    private void MoveTo(Transform inDes)
    {
        m_MoveData.TargetPosition = inDes.position;
        ChangeState(STATE.WALK);
    }

    private void Pattern1()
    {
        switch (state)
        {
            case STATE.IDLE:
                MoveTo(m_WayPoints[index++ % m_WayPoints.Length]);
                break;
            case STATE.WALK:
                Moving();
                break;
        }
    }*/
    #endregion
}
