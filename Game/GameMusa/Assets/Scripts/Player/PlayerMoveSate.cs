using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    GameObject enemy; // 적

    // 생성자 - 상속
    public PlayerMoveState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 적 찾기
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public override void Update()
    {
        base.Update();

        if (player.transform.position.x < enemy.transform.position.x) // 플레이어가 적의 왼쪽에 있음
        {
            xInput = 1; // 플레이어 방향 = 오른쪽
        }
        else if (player.transform.position.x > enemy.transform.position.x) // 플레이어가 적의 오른쪽에 있음
        {
            xInput = -1; // 플레이어 방향 = 왼쪽
        }

        // 플레이어 이동
        player.SetVelocity(xInput * player.moveSpeed, player.rb.linearVelocityY);

        if (xInput == 0 || // 좌우 방향키 미입력
            player.IsWall()) // 플레이어가 벽 감지
        {
            player.stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}