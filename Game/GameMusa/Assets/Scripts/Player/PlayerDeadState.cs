public class PlayerDeadState : PlayerState
{
    // 생성자 - 상속
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

        // 플레이어 정지
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }
}