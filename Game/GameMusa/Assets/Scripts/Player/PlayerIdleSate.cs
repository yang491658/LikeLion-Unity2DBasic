public class PlayerIdleState : PlayerGroundedState
{
    // 생성자 - 상속
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 플레이어 정지
        //rb.linearVelocity = new Vector2(0, 0);
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0) // 좌우 방향키 입력
        {
            player.stateMachine.Change(player.moveState); // 플레이어 이동 상태로 변경
        }

        if (xInput == player.direction && player.IsWall()) // 플레이어가 벽을 향해 이동
        {
            return; // 플레이어 상태 변경 없음
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}