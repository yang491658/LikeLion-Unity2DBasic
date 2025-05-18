public class PlayerAirState : PlayerState
{
    // 생성자 - 상속
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, string _animName)
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

        //if (rb.linearVelocityY == 0) // 플레이어가 낙하 종료
        if (player.IsGround()) // 플레이어가 바닥 감지
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }

        if (player.IsWall()) // 플레이어가 벽 감지
        {
            stateMachine.Change(player.wallSlideState); // 플레이어 벽타기 상태로 변경
        }

        if (xInput != 0) // 좌우 방향키 입력
        {
            // 플레이어 천천히 이동
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.linearVelocityY);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}