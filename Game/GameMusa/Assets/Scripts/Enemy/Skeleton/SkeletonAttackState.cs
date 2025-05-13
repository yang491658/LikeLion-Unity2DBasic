using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    protected Skeleton enemy; // 해골

    // 생성자 - 상속
    public SkeletonAttackState(
        EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName, Skeleton _enemy)
        : base(_stateMachine, _enemyBase, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 해골 정지
        enemy.SetZeroVelocity();

        if (trigger) // 트리거 발동
        {
            stateMachine.Change(enemy.battleState); // 해골 전투 상태로 변경
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastAttack = Time.time; // 마지막 공격 시간 저장
    }
}