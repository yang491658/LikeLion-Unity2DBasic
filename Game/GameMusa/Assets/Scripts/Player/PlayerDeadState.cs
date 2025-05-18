public class PlayerDeadState : PlayerState
{
    // ������ - ���
    public PlayerDeadState(PlayerStateMachine _stateMachine, Player _player, string _animName)
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

        // �÷��̾� ����
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }
}