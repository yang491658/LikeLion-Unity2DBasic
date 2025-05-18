using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // 생성자 - 상속
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animName)
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

        if (xInput != 0 && // 좌우 방향키 입력
            player.direction != xInput) // 입력한 방향키 = 플레이어 반대 방향
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }

        if (yInput < 0) // 아래 방향키 입력
        {
            // 플레이어 하강
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else // 아래 방향키 미입력
        {
            // 플레이어 벽타기
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY * 0.7f);
        }

        if (player.IsGround()) // 플레이어가 바닥 감지
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }

        if (Input.GetKey(KeyCode.Space)) // 스페이스 입력 유지
        {
            stateMachine.Change(player.wallJumpState); // 플레이어 벽점프 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}