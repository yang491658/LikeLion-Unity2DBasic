using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // 생성자 - 상속
    public PlayerGroundedState(PlayerStateMachine _stateMachine, Player _player, string _animName)
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

        if (player.IsGround() && // 플레이어가 바닥 감지
            Input.GetKey(KeyCode.Space)) // 스페이스 입력 유지
        {
            stateMachine.Change(player.jumpState); // 플레이어 점프 상태로 변경
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift)) // 왼쪽 쉬프트 입력
        //{
        //    stateMachine.ChangeState(player.dashState); // 플레이어 대쉬 상태로 변경
        //}

        if (!player.IsGround()) // 플레이어가 바닥 미감지
        {
            stateMachine.Change(player.airState); // 플레이어 공중 상태로 변경
        }

        if (player.IsGround() && // 플레이어가 바닥 감지
            Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 좌클릭
        {
            stateMachine.Change(player.attackState); // 플레이어 공격 상태로 변경
        }

        if (Input.GetKeyDown(KeyCode.E)) // E키 입력
        {
            stateMachine.Change(player.counterState); // 플레이어 반격 상태로 변경
        }

        //if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 중
        //if (Input.GetMouseButtonDown(1) && !player.sword) // 마우스 우클릭 중 + 소드 보유 중
        if (Input.GetMouseButtonDown(1) && // 마우스 우클릭 중
            HasSword()) // 소드 보유 중
        {
            stateMachine.Change(player.aimState); // 플레이어 조준 상태로 변경
        }

        if (Input.GetKeyDown(KeyCode.G)) // G키 입력
        {
            stateMachine.Change(player.blackholeState); // 플레이어 블랙홀 상태로 변경
        }

        if (player.IsEnemy() || player.IsEnemyOpposite()) // 플레이어가 적 감지
        {
            player.Dash(); // 플레이어 대쉬

            // 적과의 거리 계산
            float distanceToEnemy = 0;
            if (player.IsEnemy()) distanceToEnemy = player.IsEnemy().distance;
            else distanceToEnemy = player.IsEnemyOpposite().distance;

            if (distanceToEnemy < player.attackDistance) // 해골이 플레이어의 공격 거리 이내
            {
                stateMachine.Change(player.attackState); // 플레이어 공격 상태로 변경
            }
            else
            {
                stateMachine.Change(player.moveState); // 플레이어 이동 상태로 변경
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    // 소드 보유 함수
    private bool HasSword()
    {
        // 소드 있음
        if (!player.sword) return true;

        // 소드 없음
        player.sword.GetComponent<SwordSkillController>().ReturnSword(); // 소드 회수
        return false;
    }
}