using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Skeleton enemy; // �ذ�
    protected Transform player; // �÷��̾�

    // ������ - ���
    public SkeletonGroundedState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // �÷��̾� ã��
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        //if (enemy.IsPlayer()) // �ذ��� �÷��̾� ����
        if (enemy.IsPlayer() || // �ذ��� �÷��̾� ����
            Vector2.Distance(enemy.transform.position, player.position) < 2) // �ذ��� �÷��̾ ����
        {
            stateMachine.Change(enemy.battleState); // �ذ� ���� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}