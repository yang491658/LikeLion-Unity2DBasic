using UnityEngine;

public class PlayerJumpState : PlayerState
{
    // ������ - ���
    public PlayerJumpState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �÷��̾� ����
        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0) // �÷��̾ ���� ��
        {
            stateMachine.Change(player.airState); // �÷��̾� ���� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}