using UnityEngine;

public class Skeleton : Enemy
{
    #region ����
    public SkeletonIdleState idleState { get; private set; } // �ذ� ��� ����
    public SkeletonMoveState moveState { get; private set; } // �ذ� �̵� ����
    public SkeletonBattleState battleState { get; private set; } // �ذ� ���� ����
    public SkeletonAttackState attackState { get; private set; } // �ذ� ���� ����
    public SkeletonStunState stunState { get; private set; } // �ذ� ���� ����
    public SkeletonDeadState deadState { get; private set; } // �ذ� ��� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // �� ���� �ν��Ͻ� ����
        idleState = new SkeletonIdleState(stateMachine, this, "Idle", this); // �ذ� ��� ����
        moveState = new SkeletonMoveState(stateMachine, this, "Move", this); // �ذ� �̵� ����
        battleState = new SkeletonBattleState(stateMachine, this, "Move", this); // �ذ� ���� ����
        attackState = new SkeletonAttackState(stateMachine, this, "Attack", this); // �ذ� ���� ����
        stunState = new SkeletonStunState(stateMachine, this, "Stun", this); // �ذ� ���� ����
        deadState = new SkeletonDeadState(stateMachine, this, "Idle", this); // �ذ� ��� ����
    }

    protected override void Start()
    {
        base.Start();

        // ���� �ӽ� �ʱ�ȭ = �ذ� ��� ����
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.P)) // PŰ �Է�
        {
            stateMachine.Change(stunState); // �ذ� ���� ���·� ����
        }
    }

    // �ݰ� ���� �Լ�
    public override bool CanCounter()
    {
        if (base.CanCounter()) // �ݰ� ����
        {
            stateMachine.Change(stunState); // �ذ� ���� ���·� ����
            return true;
        }

        return false;
    }

    // ��� �Լ�
    public override void Die()
    {
        base.Die();

        stateMachine.Change(deadState); // �ذ� ��� ���·� ����
    }
}