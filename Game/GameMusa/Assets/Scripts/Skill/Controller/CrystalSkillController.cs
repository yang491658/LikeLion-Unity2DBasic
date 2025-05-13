using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    // 컴포넌트
    private CircleCollider2D col => GetComponent<CircleCollider2D>();
    private Animator anim => GetComponent<Animator>();

    private float crystalTimer; // 크리스탈 타이머

    [Header("폭발 정보")]
    [SerializeField] private float growSpeed; // 팽창 속도
    private bool canGrow; // 팽창 가능 여부
    private bool isExploding; // 폭발 여부

    [Header("이동 정보")]
    private float moveSpeed; // 이동 속도
    private bool isMoving; // 이동 여부
    private Transform target; // 타겟
    [SerializeField] private LayerMask enemy; // 적 레이어

    private void Update()
    {
        crystalTimer -= Time.deltaTime; // 크리스탈 타이머 감소

        if (crystalTimer < 0) // 크리스탈 지속시간 종료
        {
            //DestroyCrystal(); // 크리스탈 제거
            FinishSkill(); // 스킬 종료
        }

        if (isMoving) // 이동 중
        {
            // 크리스탈 이동
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.transform.position,
                moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.transform.position) < 1) // 크리스탈이 타겟에 근접
            {
                isMoving = false; // 이동 종료
                FinishSkill(); // 스킬 종료
            }
        }

        if (canGrow) // 팽창 가능
        {
            // 크리스탈 팽창
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(3, 3),
                growSpeed * Time.deltaTime);
        }
    }

    // 스킬 종료 함수
    public void FinishSkill()
    {
        if (isExploding) // 폭발 중
        {
            canGrow = true; // 팽창 가능
            anim.SetTrigger("Explode"); // 크리스탈 애니메이션 변경
        }
        else
        {
            DestroyCrystal(); // 크리스탈 제거
        }
    }

    // 타겟 선택 함수
    public void ChoiceTarget()
    {
        // 타겟 감지 범위
        float radius = SkillManager.instance.blackhole.GetSize() / 2;

        // 콜라이더 형성 = 타겟 감지 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemy);

        if (colliders.Length > 0) // 타겟 있음
        {
            target = colliders[Random.Range(0, colliders.Length)].transform; // 랜덤 타겟
        }
    }

    // 크리스탈 설정 함수
    //public void SetCrystal(float _crystalDuration)
    //public void SetCrystal(float _crystalDuration, bool _isExploding)
    public void SetCrystal(float _crystalDuration, bool _isExploding, 
        float _moveSpeed, bool _isMoving, Transform _target)
    {
        crystalTimer = _crystalDuration; // 크리스탈 타이머 초기화 = 크리스탈 지속시간

        isExploding = _isExploding;

        moveSpeed = _moveSpeed;
        isMoving = _isMoving;
        target = _target;
    }

    // 크리스탈 제거 함수
    public void DestroyCrystal() => Destroy(gameObject);

    // 공격 트리거 함수 이벤트
    private void AttackTrigger()
    {
        // 콜라이더 형성 = 폭발 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, col.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // 적과 접촉
            {
                // 적 데미지 이펙트
                hit.GetComponent<Enemy>().DamageEffect();
            }
        }
    }
}