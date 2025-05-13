using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Skeleton enemy; // 해골
    protected Transform player; // 플레이어

    // 생성자 - 상속
    public SkeletonGroundedState(
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

        //if (enemy.IsPlayer()) // 해골이 플레이어 감지
        if (enemy.IsPlayer() || // 해골이 플레이어 감지
            Vector2.Distance(enemy.transform.position, player.position) < 2) // 해골이 플레이어에 근접
        {
            stateMachine.Change(enemy.battleState); // 해골 전투 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}