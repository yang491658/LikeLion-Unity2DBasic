public class PlayerMoveState : PlayerGroundedState
{
    // ������ - ���
    public PlayerMoveState(PlayerStateMachine _stateMachine, Player _player, string _animName)
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

        // �÷��̾� �̵�
        player.SetVelocity(xInput * player.moveSpeed, player.rb.linearVelocityY);

        if (xInput == 0 || // �¿� ����Ű ���Է�
            player.IsWall()) // �÷��̾ �� ����
        {
            player.stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}