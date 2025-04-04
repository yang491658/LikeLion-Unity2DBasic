using UnityEngine;

public class Player : Entity
{
    private float xInput;

    [Header("이동 정보")]
    [SerializeField] private float speed; // 속도
    [SerializeField] private float jump; // 점프력

    [Header("대쉬 정보")]
    [SerializeField] private float dashSpeed; // 대쉬 속도
    [SerializeField] private float dashDuration; // 대쉬 지속 시간
    private float dashTimer; // 대쉬 발동 타이머
    [SerializeField] private float dashCoolTime; // 대쉬 쿨타임
    private float dashCooldownTimer; // 대쉬 쿨타임 타이머

    [Header("공격 정보")]
    [SerializeField] private float comboTime = 0.3f; // 콤보 지속 시간
    private bool attack; // 공격 여부
    private int combo; // 콤보
    private float comboTimer; // 콤보 타이머

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        InputKey(); // 키 입력
        Move(); // 플레이어 이동
        FlipControl(); // 플레이어 이동 중 방향 전환

        // 대쉬 타이머
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        // 콤보 타이머
        comboTimer -= Time.deltaTime;

        AnimationControl(); // 플레이어 이동 모션
    }

    private void InputKey() // 키 입력
    {
        xInput = Input.GetAxisRaw("Horizontal");

        // 플레이어 방향 전환
        if (Input.GetKeyDown(KeyCode.R)) Flip();

        // 플레이어 점프
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        // 플레이어 대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift)) CanDash();

        // 플레이어 공격
        if (Input.GetKeyDown(KeyCode.Mouse0)) Attack();
    }

    private void Move() // 이동 및 대쉬
    {
        if (attack)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTimer > 0)
        {
            rb.linearVelocity = new Vector2(xInput * dashSpeed, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocityY);
        }
    }

    private void FlipControl() // 방향 전환 컨트롤
    {
        if (rb.linearVelocityX > 0 && !isRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && isRight)
        {
            Flip();
        }
    }

    private void Jump() // 점프
    {
        if (isGround) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    private void CanDash() // 대쉬 가능
    {
        if (dashCooldownTimer < 0 && !attack)
        {
            dashTimer = dashDuration;
            dashCooldownTimer = dashCoolTime;
        }
    }

    private void Attack() // 공격
    {
        if (!isGround) return; // 공격 불가능

        if (comboTimer < 0) combo = 0; // 콤보 초기화

        attack = true;

        comboTimer = comboTime;
    }

    public void AttackOver() // 공격 종료
    {
        attack = false;

        combo++; // 콤보 상승

        if (combo > 2) combo = 0; // 콤보 초기화
    }

    private void AnimationControl() // 애니메이션 컨트롤
    {
        // 이동 모션
        bool move = rb.linearVelocity.x != 0;
        ani.SetBool("Move", move);

        // 점프 및 낙하 모션
        ani.SetBool("IsGround", isGround);
        ani.SetFloat("ySpeed", rb.linearVelocityY);

        // 대쉬 모션
        ani.SetBool("Dash", dashTimer > 0);

        // 공격 모션
        ani.SetBool("Attack", attack);
        ani.SetInteger("Combo", combo);
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
    }
}