using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region 컴포넌트
    public SpriteRenderer sr { get; private set; } // 스프라이트 렌더러
    public Animator anim { get; private set; } // 애니메이터
    public Collider2D col { get; private set; } // 콜라이더
    public Rigidbody2D rb { get; private set; } // 리지드바디
    public EntityFX fx { get; private set; } // 특수효과
    public CharacterStats stats { get; private set; } // 캐릭터 스탯
    #endregion

    public int direction { get; private set; } = 1; // 방향
    protected bool isRight = true; // 방향 = 오른쪽

    [Header("넉백 정보")]
    [SerializeField] protected Vector2 knockbackDirection; // 넉백 방향
    [SerializeField] protected float knockbackDuration; // 넉백 지속시간
    protected bool isKnock; // 넉백 여부

    [Header("충돌 정보")]
    [SerializeField] protected LayerMask groundLayer; // 바닥 레이어
    [SerializeField] protected Transform groundCheck; // 바닥 감지
    [SerializeField] protected float groundDistance; // 바닥 감지 거리
    [SerializeField] protected Transform wallCheck; // 벽 감지
    [SerializeField] protected float wallDistance; // 벽 감지 거리
    public Transform attackCheck; // 공격 감지
    public float attackRadius; // 공격 감지 범위

    public System.Action onFlip; // 방향 전환 델리게이트

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        // 컴포넌트 가져오기
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
    }

    // 데미지 이펙트 함수
    public virtual void DamageEffect()
    {
        // 깜빡임 코루틴
        fx.StartCoroutine("Blink");

        // 넉백 코루틴
        StartCoroutine(KnockBack());
    }

    // 넉백 코루틴
    protected virtual IEnumerator KnockBack()
    {
        isKnock = true; // 넉백 중

        // 엔티티 넉백
        rb.linearVelocity = new Vector2(-direction * knockbackDirection.x, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);

        isKnock = false; // 넉백 종료
    }

    #region 속도
    // 속도 설정 함수
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnock) return; // 넉백 시 종료

        // 엔티티 이동
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);

        FlipControl(_xVelocity); // 방향 전환 컨트롤
    }

    // 정지 함수
    public virtual void SetZeroVelocity()
    {
        if (isKnock) return; // 넉백 시 종료

        // 엔티티 정지
        rb.linearVelocity = new Vector2(0, 0);
    }

    // 둔화 함수
    public virtual void Slow(float _slowPercentage, float _slowDuration)
    {
    }

    // 둔화 취소 함수
    protected virtual void CancelSlow()
    {
        anim.speed = 1;
    }
    #endregion

    #region 방향 전환
    // 방향 전환 함수
    public virtual void Flip()
    {
        direction = direction * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);

        // 방향 전환 델리게이트 메서드 호출
        onFlip?.Invoke();
    }

    // 방향 컨트롤 함수 : 이동 시 방향 전환
    public virtual void FlipControl(float _x)
    {
        if (_x > 0 && !isRight)
            Flip(); // 방향 전환
        else if (_x < 0 && isRight)
            Flip(); // 방향 전환
    }
    #endregion

    #region 충돌
    // 기즈모 그리기 함수
    protected virtual void OnDrawGizmos()
    {
        // 바닥
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));

        // 벽
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + direction * wallDistance, wallCheck.position.y));

        // 공격
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }

    // 바닥 감지 함수
    public virtual bool IsGround()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

    // 벽 감지 함수
    public virtual bool IsWall()
        => Physics2D.Raycast(wallCheck.position, Vector2.right, direction * wallDistance, groundLayer);
    #endregion

    // 투명화 함수
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear; // 투명화
        else
            sr.color = Color.white; // 원상복구
    }

    // 사망 함수
    public virtual void Die()
    {
    }
}