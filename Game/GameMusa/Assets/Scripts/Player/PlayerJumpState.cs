using UnityEngine;

public class PlayerJumpState : PlayerState
{
    // 생성자 - 상속
    public PlayerJumpState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 플레이어 점프
        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0) // 플레이어가 낙하 중
        {
            stateMachine.Change(player.airState); // 플레이어 공중 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}