public class SkeletonMoveState : SkeletonGroundedState
{
    // ������ - ���
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

        // �ذ� �̵�
        enemy.SetVelocity(enemyBase.direction * enemy.moveSpeed, enemy.rb.linearVelocityY); 

        if (!enemy.IsGround() || enemy.IsWall()) // �ذ��� �ٴ� �̰��� �Ǵ� �� ����
        {
            // �ذ� ���� ��ȯ
            enemy.Flip();

            stateMachine.Change(enemy.idleState); // �ذ� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}