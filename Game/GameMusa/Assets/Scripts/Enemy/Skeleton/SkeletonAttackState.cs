using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    protected Skeleton enemy; // �ذ�

    // ������ - ���
    public SkeletonAttackState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // �ذ� ����
        enemy.SetZeroVelocity();

        if (trigger) // Ʈ���� �ߵ�
        {
            stateMachine.Change(enemy.battleState); // �ذ� ���� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastAttack = Time.time; // ������ ���� �ð� ����
    }
}