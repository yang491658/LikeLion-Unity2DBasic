using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton enemy; // 해골
    private Transform player; // 플레이어

    private int moveDirection; // 이동 방향

    // 생성자 - 상속
    public SkeletonBattleState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // 플레이어 찾기
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        // 해골 방향 설정
        if (enemy.transform.position.x < player.position.x) // 해골이 플레이어의 왼쪽에 있음
        {
            moveDirection = 1; // 해골 방향 = 오른쪽
        }
        else if (enemy.transform.position.x < player.position.x) // 해골이 플레이어의 오른쪽에 있음
        {
            moveDirection = -1; // 해골 방향 = 왼쪽
        }

        // 해골 이동
        enemy.SetVelocity(moveDirection * enemy.moveSpeed, rb.linearVelocity.y);

        if (enemy.IsPlayer()) // 해골이 플레이어 감지
        {
            stateTimer = enemy.battleTime; // 상태 타이머 초기화 = 전투 시간

            if (enemy.IsPlayer().distance < enemy.battleDistance) // 플레이어가 해골의 전투 거리 이내
            {
                if (CanAttack()) // 공격 가능
                {
                    stateMachine.Change(enemy.attackState); // 해골 공격 상태로 변경
                }
            }
        }
        else // 해골이 플레이어 미감지
        {
            if (stateTimer < 0 || // 전투 시간 종료
                Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            // 플레이어가 해골에서 멀어짐
            {
                stateMachine.Change(enemy.idleState); // 해골 대기 상태로 변경
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    // 공격 가능 함수
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastAttack + enemy.attackCoolDown) // 공격 쿨다운 종료
        {
            enemy.lastAttack = Time.time; // 해골 마지막 공격 저장
            return true; // 해골 공격 가능
        }

        return false; // 해골 공격 불가능
    }
}