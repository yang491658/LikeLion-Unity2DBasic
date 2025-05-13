using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float gravitySave; // 중력 저장
    private float flyDuration = 0.4f; // 비행 지속시간
    private bool isUsingSkill; // 스킬 사용 여부

    // 생성자 - 상속
    public PlayerBlackholeState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        gravitySave = rb.gravityScale; // 중력 저장

        // 플레이어 중력 제거
        rb.gravityScale = 0;

        stateTimer = flyDuration; // 상태 타이머 초기화 = 비행 지속시간

        isUsingSkill = false; // 스킬 미사용
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) // 비행 중
        {
            // 플레이어 상승
            rb.linearVelocity = new Vector2(0, 10);
        }

        if (stateTimer < 0) // 비행 지속시간 종료
        {
            // 플레이어 천천히 하강
            rb.linearVelocity = new Vector2(0, -0.01f);

            if (!isUsingSkill && // 스킬 미사용
                player.skill.blackhole.CanUseSkill()) // 블랙홀 스킬 사용 가능
            {
                isUsingSkill = true; // 스킬 사용 시작
            }
        }

        if (player.skill.blackhole.IsFinishSkill()) // 스킬 종료
        {
            stateMachine.Change(player.airState); // 플레이어 공중 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        // 플레이어 중력 원상복구
        rb.gravityScale = gravitySave;

        // 플레이어 투명화 해제
        player.MakeTransparent(false);
    }
}