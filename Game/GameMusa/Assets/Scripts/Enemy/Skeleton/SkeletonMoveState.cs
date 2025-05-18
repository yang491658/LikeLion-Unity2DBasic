public class SkeletonMoveState : SkeletonGroundedState
{
    // 생성자 - 상속
    public SkeletonMoveState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy) 
        : base(_stateMachine, _enemyBase, _animName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 해골 이동
        enemy.SetVelocity(enemyBase.direction * enemy.moveSpeed, enemy.rb.linearVelocityY); 

        if (!enemy.IsGround() || enemy.IsWall()) // 해골이 바닥 미감지 또는 벽 감지
        {
            // 해골 방향 전환
            enemy.Flip();

            stateMachine.Change(enemy.idleState); // 해골 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}