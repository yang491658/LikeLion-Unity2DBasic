public class PlayerDashState : PlayerState
{
    // 생성자 - 상속
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration; // 상태 타이머 초기화 = 대쉬 지속시간

        // 클론 생성
        //player.skill.clone.CreateClone(player.transform);
        //player.skill.clone.CreateClone(player.transform, Vector3.zero);
        player.skill.clone.CreateCloneOnDash();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGround() && player.IsWall()) // 플레이어가 바닥 미감지 + 벽 감지
        {
            player.stateMachine.Change(player.wallSlideState); // 플레이어 벽타기 상태로 변경
        }

        // 플레이어 대쉬
        //player.SetVelocity(player.direction * player.dashSpeed, player.rb.linearVelocityY);
        player.SetVelocity(player.dashDirection * player.dashSpeed, 0);

        if (stateTimer < 0) // 대쉬 지속시간 종료
        {
            player.stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 클론 생성
        player.skill.clone.CreateCloneOnDashOver();

        // 플레이어 정지
        player.SetVelocity(0, rb.linearVelocityY);
    }
}