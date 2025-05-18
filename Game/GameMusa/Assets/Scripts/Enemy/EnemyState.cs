using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine; // 적 상태머신
    protected Enemy enemyBase; // 적
    protected Rigidbody2D rb; // 리지드바디

    private string animName; // 애니메이션 이름
    protected float stateTimer; // 상태 타이머
    protected bool trigger; // 트리거 발동 여부

    // 생성자
    public EnemyState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName)
    {
        this.stateMachine = _stateMachine;
        this.enemyBase = _enemyBase;
        this.animName = _animName;
    }

    public virtual void Enter()
    {
        rb = enemyBase.rb; // 적의 리지드바디

        enemyBase.anim.SetBool(animName, true); // 적 애니메이션 변경

        trigger = false; // 트리거 초기화
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // 상태 타이머 감소
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animName, false); // 적 애니메이션 변경

        enemyBase.AssignAnim(animName); // 적 애니메이션 할당
    }

    // 애니메이션 트리거 함수
    public virtual void AnimationTrigger()
    {
        trigger = true; // 트리거 발동
    }
}