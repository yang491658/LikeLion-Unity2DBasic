using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public bool isActing { get; private set; } // 행동 여부

    [Header("이동 정보")]
    public float moveSpeed = 12; // 이동 속도
    public float jumpForce; // 점프력
    private float moveSpeedSave; // 이동 속도 저장
    private float jumpForceSave; // 점프력 저장

    [Header("대쉬 정보")]
    public float dashSpeed; // 대쉬 속도
    private float dashSpeedSave; // 대쉬 속도 저장
    public float dashDirection { get; private set; } // 대쉬 방향
    public float dashDuration; // 대쉬 지속시간
    [SerializeField] private float dashCooldown; // 대쉬 쿨다운
    public float dashTimer; // 대쉬 타이머

    [Header("공격 정보")]
    public Vector2[] attackMovement; // 공격 움직임
    public float counterDuration = 0.2f; // 반격 지속시간

    public SkillManager skill { get; private set; } // 스킬 매니저
    public GameObject sword { get; private set; } // 보유 중인 소드

    [Header("스킬 정보")]
    public float swordReturnImpact; // 소드 회수 반동

    [Header("자동 전투 정보")]
    [SerializeField] protected LayerMask enemyLayer; // 적 레이어
    public float attackDistance; // 공격 거리
    public float attackCooldown; // 공격 쿨다운
    [HideInInspector] public float lastAttack; // 마지막 공격

    #region 상태
    public PlayerStateMachine stateMachine { get; private set; } // 플레이어 상태머신

    // 플레이어 상태
    public PlayerIdleState idleState { get; private set; } // 플레이어 대기 상태
    public PlayerMoveState moveState { get; private set; } // 플레이어 이동 상태
    public PlayerAirState airState { get; private set; } // 플레이어 공중 상태
    public PlayerJumpState jumpState { get; private set; } // 플레이어 점프 상태
    public PlayerDashState dashState { get; private set; } // 플레이어 대쉬 상태
    public PlayerWallSlideState wallSlideState { get; private set; } // 플레이어 벽타기 상태
    public PlayerWallJumpState wallJumpState { get; private set; } // 플레이어 벽점프 상태
    public PlayerAttackState attackState { get; private set; } // 플레이어 공격 상태
    public PlayerCounterState counterState { get; private set; } // 플레이어 반격 상태
    public PlayerAimState aimState { get; private set; } // 플레이어 조준 상태
    public PlayerCatchState catchState { get; private set; } // 플레이어 잡기 상태
    public PlayerBlackholeState blackholeState { get; private set; } // 플레이어 블랙홀 상태
    public PlayerDeadState deadState { get; private set; } // 플레이어 사망 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();

        // 각 상태 인스턴스 생성
        idleState = new PlayerIdleState(stateMachine, this, "Idle"); // 플레이어 대기 상태
        moveState = new PlayerMoveState(stateMachine, this, "Move"); // 플레이어 이동 상태
        airState = new PlayerAirState(stateMachine, this, "Jump"); // 플레이어 공중 상태
        jumpState = new PlayerJumpState(stateMachine, this, "Jump"); // 플레이어 점프 상태
        dashState = new PlayerDashState(stateMachine, this, "Dash"); // 플레이어 대쉬 상태
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide"); // 플레이어 벽타기 상태
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump"); // 플레이어 벽점프 상태
        attackState = new PlayerAttackState(stateMachine, this, "Attack"); // 플레이어 공격 상태
        counterState = new PlayerCounterState(stateMachine, this, "Counter"); // 플레이어 반격 상태
        aimState = new PlayerAimState(stateMachine, this, "Aim"); // 플레이어 조준 상태
        catchState = new PlayerCatchState(stateMachine, this, "Catch"); // 플레이어 잡기 상태
        blackholeState = new PlayerBlackholeState(stateMachine, this, "Jump"); // 플레이어 블랙홀 상태
        deadState = new PlayerDeadState(stateMachine, this, "Dead"); // 플레이어 사망 상태
    }

    protected override void Start()
    {
        base.Start();

        // 상태머신 초기화 = 플레이어 대기 상태
        stateMachine.Initialize(idleState);

        // 스킬 매니저 초기화 (싱글톤)
        skill = SkillManager.instance;

        // 속도 정보 저장
        moveSpeedSave = moveSpeed;
        jumpForceSave = jumpForce;
        dashSpeedSave = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        Dash(); // 대쉬

        if (Input.GetKeyDown(KeyCode.H)) // H키 입력
        {
            skill.crystal.CanUseSkill(); // 크리스탈 스킬 사용 가능
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 숫자 1키 입력
        {
            Inventory.instance.UseFlask(); // 인벤토리 플라스크 사용
        }
    }

    // 둔화 함수 (상속)
    public override void Slow(float _slowPercentage, float _slowDuration)
    {
        // 둔화 적용
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        // 둔화 지속시간 종료 후 둔화 취소
        Invoke("CancelSlow", _slowDuration);
    }

    // 둔화 취소 함수 (상속)
    protected override void CancelSlow()
    {
        base.CancelSlow();

        // 속도 원상복구
        moveSpeed = moveSpeedSave;
        jumpForce = jumpForceSave;
        dashSpeed = dashSpeedSave;
    }

    // 행동 코루틴
    public IEnumerator Act(float _seconds)
    {
        isActing = true; // 행동 중
        yield return new WaitForSeconds(_seconds);
        isActing = false; // 행동 종료
    }

    // 대쉬 함수
    public void Dash()
    {
        if (IsWall()) return; // 플레이어가 벽 감지 시 종료

        dashTimer -= Time.deltaTime; // 대쉬 타이머 감소

        //if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0) // 왼쪽 쉬프트 입력 + 대쉬 쿨다운 종료
        if (dashTimer < 0 || // 대쉬 쿨다운 종료 
            Input.GetKeyDown(KeyCode.LeftShift) && // 왼쪽 쉬프트 입력
            SkillManager.instance.dash.CanUseSkill()) // 스킬 사용 가능
        {
            dashTimer = dashCooldown; // 대쉬 타이머 초기화 = 대쉬 쿨다운

            // 좌우 방향키 입력
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0) // 입력 없음
                dashDirection = direction;

            stateMachine.Change(dashState); // 플레이어 대쉬 상태로 변경
        }
    }

    // 소드 할당 함수
    public void AssignSword(GameObject _sword) => sword = _sword;

    // 소드 잡기 함수
    public void CatchSwrod()
    {
        stateMachine.Change(catchState); // 플레이어 잡기 상태로 변경
        Destroy(sword); // 소드 제거
    }

    // 사망 함수
    public override void Die()
    {
        base.Die();

        stateMachine.Change(deadState); // 플레이어 사망 상태로 변경
    }

    // 적 감지 함수
    public virtual RaycastHit2D IsEnemy()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, 50, enemyLayer);
    public virtual RaycastHit2D IsEnemyOpposite()
        => Physics2D.Raycast(wallCheck.position, Vector2.left * direction, 50, enemyLayer);

    // 애니메이션 트리거 함수
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
}