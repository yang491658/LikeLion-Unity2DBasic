using UnityEngine;

public class SkeletonStunState : EnemyState
{
    protected Skeleton enemy; // 해골

    // 생성자 - 상속
    public SkeletonStunState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // 해골 빨강 깜박임 : 반복 실행
        enemy.fx.InvokeRepeating("BlinkRed", 0, 0.1f);

        stateTimer = enemy.stunDuration; // 상태 타이머 초기화 = 기절 지속시간

        // 해골 넉백
        rb.linearVelocity = new Vector2(-enemy.direction * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // 기절 지속시간 종료
        {
            stateMachine.Change(enemy.idleState); // 해골 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 해골 깜빡임 종료
        enemy.fx.Invoke("CancelBlink", 0);
    }
}