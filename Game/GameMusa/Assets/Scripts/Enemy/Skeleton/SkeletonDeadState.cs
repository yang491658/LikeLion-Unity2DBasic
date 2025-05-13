using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    protected Skeleton enemy; // 해골

    // 생성자 - 상속
    public SkeletonDeadState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool(enemy.animName, true); // 해골 애니메이션 변경
        enemy.anim.speed = 0; // 해골 애니메이션 정지
        enemy.col.enabled = false; // 해골 콜라이더 해제

        stateTimer = 0.1f; // 상태 타이머 초기화 = 사망 지속 시간
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) // 사망 지속시간 종료
        {
            // 해골 낙하
            rb.linearVelocity = new Vector2(0, 10);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}