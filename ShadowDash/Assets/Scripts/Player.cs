using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // 리지드바디
    private Animator ani; // 애니메이터

    private float xInput;

    [SerializeField] private float speed; // 속도
    [SerializeField] private float jump; // 점프력

    private int dircetion = 1; // 방향
    private bool isRight = true;

    [Header("충돌 정보")]
    [SerializeField] private float distance; // 충돌 감지 거리
    [SerializeField] private LayerMask ground;
    private bool isGround; // 충돌 여부

    [Header("대쉬 정보")]
    [SerializeField] private float dashSpeed; // 대쉬 속도
    [SerializeField] private float dashDuration; // 대쉬 지속 시간
    private float dashTimer; // 대쉬 발동 타이머
    [SerializeField] private float dashCooldown; // 대쉬 쿨타임
    private float dashCooldownTimer; // 대쉬 쿨타임 타이머

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputKey(); // 키 입력
        Move(); // 플레이어 이동
        FlipControl(); // 플레이어 이동 중 방향 전환
        CollisionCheck(); // 충돌 체크

        // 대쉬 타이머
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CanDash();
        }
    }

    private void CanDash() // 대쉬 가능
    {
        if (dashCooldownTimer < 0)
        {
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void Move() // 이동 및 대쉬
    {
        if (dashTimer > 0)
        {
            rb.linearVelocity = new Vector2(xInput * dashSpeed, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocityY);
        }
    }

    private void Flip() // 방향 전환
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
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

    private void OnDrawGizmos() // 기즈모 그리기
    {
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x,
            transform.position.y - distance));
    }

    private void CollisionCheck() // 충돌 감지
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
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
    }
}