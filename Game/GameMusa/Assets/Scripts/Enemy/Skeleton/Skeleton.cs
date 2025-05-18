using UnityEngine;

public class Skeleton : Enemy
{
    #region 상태
    public SkeletonIdleState idleState { get; private set; } // 해골 대기 상태
    public SkeletonMoveState moveState { get; private set; } // 해골 이동 상태
    public SkeletonBattleState battleState { get; private set; } // 해골 전투 상태
    public SkeletonAttackState attackState { get; private set; } // 해골 공격 상태
    public SkeletonStunState stunState { get; private set; } // 해골 기절 상태
    public SkeletonDeadState deadState { get; private set; } // 해골 사망 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 각 상태 인스턴스 생성
        idleState = new SkeletonIdleState(stateMachine, this, "Idle", this); // 해골 대기 상태
        moveState = new SkeletonMoveState(stateMachine, this, "Move", this); // 해골 이동 상태
        battleState = new SkeletonBattleState(stateMachine, this, "Move", this); // 해골 전투 상태
        attackState = new SkeletonAttackState(stateMachine, this, "Attack", this); // 해골 공격 상태
        stunState = new SkeletonStunState(stateMachine, this, "Stun", this); // 해골 기절 상태
        deadState = new SkeletonDeadState(stateMachine, this, "Idle", this); // 해골 사망 상태
    }

    protected override void Start()
    {
        base.Start();

        // 상태 머신 초기화 = 해골 대기 상태
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.P)) // P키 입력
        {
            stateMachine.Change(stunState); // 해골 기절 상태로 변경
        }
    }

    // 반격 가능 함수
    public override bool CanCounter()
    {
        if (base.CanCounter()) // 반격 가능
        {
            stateMachine.Change(stunState); // 해골 기절 상태로 변경
            return true;
        }

        return false;
    }

    // 사망 함수
    public override void Die()
    {
        base.Die();

        stateMachine.Change(deadState); // 해골 사망 상태로 변경
    }
}