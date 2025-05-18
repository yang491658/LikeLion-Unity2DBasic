using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int combo; // 콤보
    private float comboDuration = 2; // 콤보 지속시간
    private float lastAttack; // 마지막 공격

    // 생성자 - 상속
    public PlayerAttackState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (combo > 2 || // 최대 콤보 도달
            Time.time >= lastAttack + comboDuration) // 콤보 지속시간 종료
        {
            combo = 0; // 콤보 초기화
        }

        player.anim.SetInteger("Combo", combo); // 플레이어 애니메이션 변경

        stateTimer = 0.1f; // 상태 타이머 초기화 = 콤보 사이 간격

        // 공격 방향 선택
        float attackDirection = player.direction;
        if (xInput != 0) attackDirection = xInput; // 좌우 방향 키 입력

        // 플레이어 공격 이동
        player.SetVelocity(player.attackMovement[combo].x * attackDirection, player.attackMovement[combo].y);
    }

    public override void Update()
    {
        base.Update();

        if (trigger) // 트리거 발동
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }

        if (stateTimer < 0) // 이전 공격 종료
        {
            // 플레이어 정지
            //rb.linearVelocity = new Vector2(0, 0);
            player.SetZeroVelocity();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 플레이어 행동 코루틴
        player.StartCoroutine("Act", 0.1f);

        combo++; // 콤보 상승
        lastAttack = Time.time; // 마지막 공격 저장
    }
}