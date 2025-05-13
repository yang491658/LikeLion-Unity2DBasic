public class PlayerWallJumpState : PlayerState
{
    // 생성자 - 상속
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f; // 상태 타이머 초기화

        // 플레이어 벽점프
        player.SetVelocity(-player.direction * 5, player.jumpForce * 1.2f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) // 벽점프 지속시간 종료
        {
            player.stateMachine.Change(player.airState); // 플레이어 공중 상태로 변경
        }

        if (player.IsGround()) // 플레이어가 바닥 감지
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
