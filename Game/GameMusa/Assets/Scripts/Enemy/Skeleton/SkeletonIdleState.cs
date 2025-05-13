public class SkeletonIdleState : SkeletonGroundedState
{
    // 생성자 - 상속
    public SkeletonIdleState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime; // 상태 타이머 초기화 = 대기 시간
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // 대기 시간 종료
        {
            stateMachine.Change(enemy.moveState); // 해골 이동 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}