using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton enemy; // �ذ�
    private Transform player; // �÷��̾�

    private int moveDirection; // �̵� ����

    // ������ - ���
    public SkeletonBattleState(
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

        // �ذ� ���� ����
        if (enemy.transform.position.x < player.position.x) // �ذ��� �÷��̾��� ���ʿ� ����
        {
            moveDirection = 1; // �ذ� ���� = ������
        }
        else if (enemy.transform.position.x < player.position.x) // �ذ��� �÷��̾��� �����ʿ� ����
        {
            moveDirection = -1; // �ذ� ���� = ����
        }

        // �ذ� �̵�
        enemy.SetVelocity(moveDirection * enemy.moveSpeed, rb.linearVelocity.y);

        if (enemy.IsPlayer()) // �ذ��� �÷��̾� ����
        {
            stateTimer = enemy.battleTime; // ���� Ÿ�̸� �ʱ�ȭ = ���� �ð�

            if (enemy.IsPlayer().distance < enemy.battleDistance) // �÷��̾ �ذ��� ���� �Ÿ� �̳�
            {
                if (CanAttack()) // ���� ����
                {
                    stateMachine.Change(enemy.attackState); // �ذ� ���� ���·� ����
                }
            }
        }
        else // �ذ��� �÷��̾� �̰���
        {
            if (stateTimer < 0 || // ���� �ð� ����
                Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            // �÷��̾ �ذ񿡼� �־���
            {
                stateMachine.Change(enemy.idleState); // �ذ� ��� ���·� ����
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    // ���� ���� �Լ�
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastAttack + enemy.attackCoolDown) // ���� ��ٿ� ����
        {
            enemy.lastAttack = Time.time; // �ذ� ������ ���� ����
            return true; // �ذ� ���� ����
        }

        return false; // �ذ� ���� �Ұ���
    }
}