public class SkeletonIdleState : SkeletonGroundedState
{
    // ������ - ���
    public SkeletonIdleState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime; // ���� Ÿ�̸� �ʱ�ȭ = ��� �ð�
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // ��� �ð� ����
        {
            stateMachine.Change(enemy.moveState); // �ذ� �̵� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}