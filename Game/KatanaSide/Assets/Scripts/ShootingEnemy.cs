using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("적 캐릭터 속성")]
    public float detectionRange = 10f; // 플레이어 감지 거리
    public float shootingInterval = 2f; // 발사 대기 시간
    public GameObject missile; // 미사일

    [Header("참조 컴포넌트")]
    private Transform player; // 플레이어 위치
    public Transform firePoint; // 미사일 발사 위치
    private float shootTimer; // 발사 타이머
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        // 컴포넌트
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();

        shootTimer = shootingInterval; // 타이머 초기화
    }


    void Update()
    {
        if (player == null) return;

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어 방향으로 스프라이트 회전
            spriteRenderer.flipX = (player.position.x < transform.position.x);

            // 미사일 발사
            shootTimer -= Time.deltaTime;   //타이머 감소

            if (shootTimer <= 0)
            {
                Shoot(); // 미사일 발사 함수 실행
                shootTimer = shootingInterval; // 타이머 리셋
            }

        }
    }

    // 미사일 발사 함수
    void Shoot()
    {
        // 미사일 생성
        GameObject go= Instantiate(missile, firePoint.position, Quaternion.identity);

        // 플레이어 방향으로 발사 방향 전환
        Vector2 direction = (player.position - firePoint.position).normalized;
        go.GetComponent<EnemyMissile>().SetDirection(direction); // 미사일 이동 방향
        go.GetComponent<SpriteRenderer>().flipX = (player.position.x < transform.position.x); // 미사일 방향 전환
    }

    // 디버깅용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    // 캐릭터 사망 함수
    public void Death()
    {
        animator.SetBool("Death", true);

        // 애니메이션 종류 후 오브젝트 제거
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}