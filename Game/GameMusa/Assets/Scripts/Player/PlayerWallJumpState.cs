public class PlayerWallJumpState : PlayerState
{
    // ������ - ���
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f; // ���� Ÿ�̸� �ʱ�ȭ

        // �÷��̾� ������
        player.SetVelocity(-player.direction * 5, player.jumpForce * 1.2f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // ������ ���ӽð� ����
        {
            player.stateMachine.Change(player.airState); // �÷��̾� ���� ���·� ����
        }

        if (player.IsGround()) // �÷��̾ �ٴ� ����
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
