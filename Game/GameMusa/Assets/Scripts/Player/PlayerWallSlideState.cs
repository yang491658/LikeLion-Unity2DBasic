using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // ������ - ���
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 && // �¿� ����Ű �Է�
            player.direction != xInput) // �Է��� ����Ű = �÷��̾� �ݴ� ����
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }

        if (yInput < 0) // �Ʒ� ����Ű �Է�
        {
            // �÷��̾� �ϰ�
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else // �Ʒ� ����Ű ���Է�
        {
            // �÷��̾� ��Ÿ��
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY * 0.7f);
        }

        if (player.IsGround()) // �÷��̾ �ٴ� ����
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }

        if (Input.GetKey(KeyCode.Space)) // �����̽� �Է� ����
        {
            stateMachine.Change(player.wallJumpState); // �÷��̾� ������ ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}