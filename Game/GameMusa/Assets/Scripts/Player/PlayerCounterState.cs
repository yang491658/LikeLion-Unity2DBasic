using UnityEngine;

public class PlayerCounterState : PlayerState
{
    private bool canCreateClone; // Ŭ�� ���� ���� ����

    // ������ - ���
    public PlayerCounterState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterDuration; // ���� Ÿ�̸� �ʱ�ȭ = �ݰ� ���ӽð�

        player.anim.SetBool("CounterSuccess", false); // �÷��̾� �ִϸ��̼� ����

        canCreateClone = true; // Ŭ�� ���� ����
    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetBool("CounterSuccess")) // �ݰ� ����
        {
            // �÷��̾� ���� �̵�
            player.SetVelocity(player.direction * 10, 0);
        }
        else // �ݰ� ����
        {
            // �÷��̾� ����
            player.SetZeroVelocity();
        }

        // �ݶ��̴� ���� = �÷��̾� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // ���� ����
            {
                if (hit.GetComponent<Enemy>().CanCounter()) // �ݰ� ����
                {
                    stateTimer = 10; // ���� Ÿ�̸� ����

                    player.anim.SetBool("CounterSuccess", true); // �÷��̾� �ִϸ��̼� ����

                    if (canCreateClone) // Ŭ�� ���� ����
                    {
                        // Ŭ�� ����
                        player.skill.clone.CreateCloneOnCounter(hit.transform);

                        canCreateClone = false; // Ŭ�� ���� �Ұ���
                    }
                }
            }
        }

        if (stateTimer < 0 ||  // �ݰ� ���ӽð� ����
            trigger) // Ʈ���� �ߵ�
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.SetBool("CounterSuccess", false); // �÷��̾� �ִϸ��̼� ����
    }
}
