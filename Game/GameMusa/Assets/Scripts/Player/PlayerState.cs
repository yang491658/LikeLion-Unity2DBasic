using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine; // 플레이어 상태머신
    protected Player player; // 플레이어

    protected Rigidbody2D rb; // 리지드바디

    private string animName; // 애니메이션 이름
    protected float xInput; // 좌우 방향 입력값
    protected float yInput; // 상하 방향 입력값
    protected float stateTimer; // 상태 타이머
    protected bool trigger; // 트리거 발동 여부

    // 생성자
    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animName = _animName;
    }

    public virtual void Enter()
    {
        rb = player.rb; // 플레이어의 리지드바디

        player.anim.SetBool(animName, true); // 플레이어 애니메이션 변경

        trigger = false; // 트리거 초기화
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // 상태 타이머 감소

        xInput = Input.GetAxisRaw("Horizontal"); // 좌우 방향키 입력
        yInput = Input.GetAxisRaw("Vertical"); // 상하 방향키 입력

        player.anim.SetFloat("ySpeed", rb.linearVelocityY); // 플레이어 애니메이션 변경
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animName, false); // 플레이어 애니메이션 변경
    }

    // 애니메이션 트리거 함수
    public virtual void AnimationTrigger()
    {
        trigger = true; // 트리거 발동
    }
}