using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    // 컴포넌트
    private CircleCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    private Player player; // 플레이어

    private bool isRotating = true; // 회전 여부

    private float returnSpeed; // 회수 속도
    private bool isReturning; // 회수 여부
    private float freezeTimeDuration; // 시간 정지 지속시간

    [Header("바운스 정보")]
    private float bounceSpeed; // 바운스 속도
    private int bounceAmount; // 바운스 횟수
    private bool isBouncing; // 바운스 여부
    private List<Transform> enemyTarget; // 타겟 배열
    private int targetIndex; // 타겟 순서

    [Header("관통 정보")]
    private float pierceAmount; // 관통 횟수

    [Header("스핀 정보")]
    private float spinDistance; // 스핀 거리
    private float spinDuration; // 스핀 지속시간
    private float spinTimer; // 스핀 타이머
    private bool isSpinning; // 스핀 여부

    private float moveDirection; // 이동 방향
    private bool isHitting; // 타격 여부
    private float hitCooldown; // 타격 쿨다운
    private float hitTimer; // 타격 타이머

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isRotating) // 회전 중
        {
            // 소드 방향 설정
            transform.right = rb.linearVelocity;
        }

        if (isReturning) // 회수 중
        {
            // 소드 이동 = 플레이어를 향해 이동
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1) // 소드가 플레이어에 근접
            {
                // 플레이어 소드 잡기
                player.CatchSwrod();
            }
        }

        BounceSword(); // 소드 바운스

        SpinSword(); // 소드 스핀
    }

    // 소드 바운스 함수
    private void BounceSword()
    {
        if (isBouncing && // 바운스 중
            enemyTarget.Count > 0) // 타겟 있음
        {
            // 소드 이동 = 타겟을 향해 이동
            transform.position = Vector2.MoveTowards(
                transform.position,
                enemyTarget[targetIndex].position,
                bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            // 소드가 타겟에 근접
            {
                DamageBySword(enemyTarget[targetIndex].GetComponent<Enemy>()); // 소드 데미지

                targetIndex++; // 다음 타겟 순서
                bounceAmount--; // 바운스 횟수 감소

                if (bounceAmount <= 0) // 바운스 횟수 없음
                {
                    isBouncing = false; // 바운스 종료
                    isReturning = true; // 회수 시작
                }

                if (targetIndex >= enemyTarget.Count) // 다음 타겟 없음
                {
                    targetIndex = 0; // 타겟 순서 초기화
                }
            }
        }
    }

    // 소드 스핀 함수
    private void SpinSword()
    {
        if (isSpinning) // 스핀 중
        {
            if (!isHitting && // 타격 중 아님
                Vector2.Distance(player.transform.position, transform.position) > spinDistance)
            // 스핀 거리 도달
            {
                StartHit(); // 타격 시작
            }

            HitBySpin(); // 스핀 타겟 타격
        }
    }

    // 타격 시작 함수
    private void StartHit()
    {
        isHitting = true; // 타격 시작

        rb.constraints = RigidbodyConstraints2D.FreezePosition; // 위치 고정

        spinTimer = spinDuration; // 스핀 타이머 초기화 = 스핀 지속시간
    }

    // 스핀 타격 함수
    private void HitBySpin()
    {
        if (isHitting) // 타격 중
        {
            // 스핀 천천히 이동
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x + moveDirection, transform.position.y),
                1.5f * Time.deltaTime);

            spinTimer -= Time.deltaTime; // 스핀 타이머 감소

            if (spinTimer < 0) // 스핀 지속시간 종료
            {
                isSpinning = false; // 스핀 종료
                isReturning = true; // 회수 시작
            }

            hitTimer -= Time.deltaTime; // 타격 타이머 감소

            if (hitTimer < 0) // 타격 쿨다운 종료
            {
                hitTimer = hitCooldown; // 타격 타이머 초기화 = 타격 쿨다운

                // 콜라이더 형성 = 스핀 타격 범위
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null) // 적과 접촉
                    {
                        // 적 데미지 이펙트
                        //hit.GetComponent<Enemy>().DamageEffect();

                        DamageBySword(hit.GetComponent<Enemy>()); // 소드 데미지
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning) return; // 회수 시 종료

        // 적과 충돌 시 적 데미지 이펙트
        //collision.GetComponent<Enemy>()?.DamageEffect();

        if (collision.GetComponent<Enemy>() != null) // 적과 충돌
        {
            DamageBySword(collision.GetComponent<Enemy>()); // 소드 데미지
        }

        SetTargetsForBounce(collision); // 바운스 타겟 설정

        StuckSword(collision); // 소드 고정
    }

    // 바운스 타겟 설정 함수
    private void SetTargetsForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && // 적과 충돌
            isBouncing && // 바운스 중
            enemyTarget.Count <= 0) // 타겟 없음
        {
            // 콜라이더 형성 = 타겟 감지 범위
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null) // 적과 접촉
                {
                    enemyTarget.Add(hit.transform); // 타겟 추가
                }
            }
        }
    }

    // 소드 고정 함수
    private void StuckSword(Collider2D collision)
    {
        if (isSpinning) // 스핀 중
        {
            StartHit(); // 타격 시작
            return; // 고정 종료
        }

        if (pierceAmount > 0 && // 관통 횟수 있음
            collision.GetComponent<Enemy>() != null) // 적과 충돌
        {
            pierceAmount--; // 관통 횟수 감소
            return; // 고정 종료
        }

        if (!isBouncing) // 바운스 중 아님
        {
            // 충돌 비활성화
            col.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // 위치 고정
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTarget.Count > 0) return; // 바운스 중 + 타겟 있을 시 고정 종료

        anim.SetBool("Rotate", false); // 소드 애니메이션 변경

        isRotating = false; // 회전 종료

        // 충돌체에 종속
        transform.parent = collision.transform;
    }

    // 소드 설정 함수
    //public void SetSword(Vector2 _dir, float _gravityScale)
    //public void SetSword(Vector2 _dir, float _gravityScale, Player _player, float _returnSpeed)
    public void SetSword
        (Vector2 _dir, float _gravityScale, Player _player, float _returnSpeed, float _freezeTimeDuration)
    {
        rb.linearVelocity = _dir; // 속도 및 방향
        rb.gravityScale = _gravityScale; // 중력

        if (pierceAmount <= 0) // 관통 횟수 없음
        {
            anim.SetBool("Rotate", true); // 소드 애니메이션 변경
        }

        player = _player; // 플레이어
        returnSpeed = _returnSpeed; // 회수 속도

        moveDirection = Mathf.Clamp(rb.linearVelocity.x, -1, 1); // 이동 방향

        freezeTimeDuration = _freezeTimeDuration; // 시건 정지 지속시간

        Invoke("DestroySword", 7); // 일정 시간 후 소드 제거
    }

    // 바운스 설정 함수
    public void SetBounce(float _bounceSpeed, int _bounceAmount, bool _isBouncing)
    {
        bounceSpeed = _bounceSpeed;
        bounceAmount = _bounceAmount;
        isBouncing = _isBouncing;

        enemyTarget = new List<Transform>();
    }

    // 관통 설정 함수
    public void SetPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    // 스핀 설정 함수
    public void SetupSpin(float _spinDistance, float _spinDuration, bool _isSpinning, float _hitCooldown)
    {
        spinDistance = _spinDistance;
        spinDuration = _spinDuration;
        isSpinning = _isSpinning;

        hitCooldown = _hitCooldown;
    }

    // 소드 회수 함수
    public void ReturnSword()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true; // 회수 시작
    }

    // 소드 데미지 함수
    private void DamageBySword(Enemy enemy)
    {
        // 적 데미지 이펙트
        enemy.DamageEffect();

        // 시간 정지 코루틴
        enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
    }

    // 소드 제거 함수
    private void DestroySword()
    {
        Destroy(gameObject); // 소드 제거
    }
}