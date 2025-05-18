using UnityEngine;

public class PlayerCatchState : PlayerState
{
    private Transform sword; // 소드

    public PlayerCatchState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 소드 위치로 플레이어 방향 회전
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.direction == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.direction == -1)
            player.Flip();

        // 플레이어 소드 잡기 반동
        rb.linearVelocity = new Vector2(-player.direction * player.swordReturnImpact, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (trigger) // 트리거 발동 시
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 플레이어 행동 코루틴
        player.StartCoroutine("Act", 0.1f);
    }
}