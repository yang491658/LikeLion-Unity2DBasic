public class PlayerIdleState : PlayerGroundedState
{
    // ������ - ���
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �÷��̾� ����
        //rb.linearVelocity = new Vector2(0, 0);
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0) // �¿� ����Ű �Է�
        {
            player.stateMachine.Change(player.moveState); // �÷��̾� �̵� ���·� ����
        }

        if (xInput == player.direction && player.IsWall()) // �÷��̾ ���� ���� �̵�
        {
            return; // �÷��̾� ���� ���� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}