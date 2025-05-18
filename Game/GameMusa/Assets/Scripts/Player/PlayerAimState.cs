using UnityEngine;

public class PlayerAimState : PlayerState
{
    // 생성자 - 상속
    public PlayerAimState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.ActiveDots(true); // 조준 경로 활성화
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1)) // 마우스 우클릭 해제
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }

        // 플레이어 정지
        player.SetZeroVelocity();

        // 마우스 위치로 플레이어 방향 회전
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.transform.position.x > mousePosition.x && player.direction == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.direction == -1)
            player.Flip();
    }

    public override void Exit()
    {
        base.Exit();

        // 플레이어 행동 코루틴
        player.StartCoroutine("Act", 0.2f);
    }
}
