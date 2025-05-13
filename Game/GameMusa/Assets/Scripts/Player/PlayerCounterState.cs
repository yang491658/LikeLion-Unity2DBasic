using UnityEngine;

public class PlayerCounterState : PlayerState
{
    private bool canCreateClone; // 클론 생성 가능 여부

    // 생성자 - 상속
    public PlayerCounterState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterDuration; // 상태 타이머 초기화 = 반격 지속시간

        player.anim.SetBool("CounterSuccess", false); // 플레이어 애니메이션 변경

        canCreateClone = true; // 클론 생성 가능
    }

    public override void Update()
    {
        base.Update();

        if (player.anim.GetBool("CounterSuccess")) // 반격 성공
        {
            // 플레이어 공격 이동
            player.SetVelocity(player.direction * 10, 0);
        }
        else // 반격 실패
        {
            // 플레이어 정지
            player.SetZeroVelocity();
        }

        // 콜라이더 형성 = 플레이어 공격 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // 적과 접촉
            {
                if (hit.GetComponent<Enemy>().CanCounter()) // 반격 가능
                {
                    stateTimer = 10; // 상태 타이머 조정

                    player.anim.SetBool("CounterSuccess", true); // 플레이어 애니메이션 변경

                    if (canCreateClone) // 클론 생성 가능
                    {
                        // 클론 생성
                        player.skill.clone.CreateCloneOnCounter(hit.transform);

                        canCreateClone = false; // 클론 생성 불가능
                    }
                }
            }
        }

        if (stateTimer < 0 ||  // 반격 지속시간 종료
            trigger) // 트리거 발동
        {
            stateMachine.Change(player.idleState); // 플레이어 대기 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.SetBool("CounterSuccess", false); // 플레이어 애니메이션 변경
    }
}
