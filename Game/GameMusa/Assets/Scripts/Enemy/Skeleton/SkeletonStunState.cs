using UnityEngine;

public class SkeletonStunState : EnemyState
{
    protected Skeleton enemy; // �ذ�

    // ������ - ���
    public SkeletonStunState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // �ذ� ���� ������ : �ݺ� ����
        enemy.fx.InvokeRepeating("BlinkRed", 0, 0.1f);

        stateTimer = enemy.stunDuration; // ���� Ÿ�̸� �ʱ�ȭ = ���� ���ӽð�

        // �ذ� �˹�
        rb.linearVelocity = new Vector2(-enemy.direction * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // ���� ���ӽð� ����
        {
            stateMachine.Change(enemy.idleState); // �ذ� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �ذ� ������ ����
        enemy.fx.Invoke("CancelBlink", 0);
    }
}