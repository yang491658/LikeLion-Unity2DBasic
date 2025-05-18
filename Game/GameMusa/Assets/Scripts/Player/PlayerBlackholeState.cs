using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float gravitySave; // �߷� ����
    private float flyDuration = 0.4f; // ���� ���ӽð�
    private bool isUsingSkill; // ��ų ��� ����

    // ������ - ���
    public PlayerBlackholeState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        gravitySave = rb.gravityScale; // �߷� ����

        // �÷��̾� �߷� ����
        rb.gravityScale = 0;

        stateTimer = flyDuration; // ���� Ÿ�̸� �ʱ�ȭ = ���� ���ӽð�

        isUsingSkill = false; // ��ų �̻��
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) // ���� ��
        {
            // �÷��̾� ���
            rb.linearVelocity = new Vector2(0, 10);
        }

        if (stateTimer < 0) // ���� ���ӽð� ����
        {
            // �÷��̾� õõ�� �ϰ�
            rb.linearVelocity = new Vector2(0, -0.01f);

            if (!isUsingSkill && // ��ų �̻��
                player.skill.blackhole.CanUseSkill()) // ��Ȧ ��ų ��� ����
            {
                isUsingSkill = true; // ��ų ��� ����
            }
        }

        if (player.skill.blackhole.IsFinishSkill()) // ��ų ����
        {
            stateMachine.Change(player.airState); // �÷��̾� ���� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �÷��̾� �߷� ���󺹱�
        rb.gravityScale = gravitySave;

        // �÷��̾� ����ȭ ����
        player.MakeTransparent(false);
    }
}