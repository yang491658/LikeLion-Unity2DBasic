using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    protected Skeleton enemy; // �ذ�

    // ������ - ���
    public SkeletonDeadState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool(enemy.animName, true); // �ذ� �ִϸ��̼� ����
        enemy.anim.speed = 0; // �ذ� �ִϸ��̼� ����
        enemy.col.enabled = false; // �ذ� �ݶ��̴� ����

        stateTimer = 0.1f; // ���� Ÿ�̸� �ʱ�ȭ = ��� ���� �ð�
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) // ��� ���ӽð� ����
        {
            // �ذ� ����
            rb.linearVelocity = new Vector2(0, 10);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}