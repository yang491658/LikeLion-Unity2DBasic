using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    // 컴포넌트
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private float disappearSpeed; // 사라지는 속도 = 투명화 속도
    private float cloneTimer; // 클론 타이머

    [SerializeField] private Transform attackCheck; // 공격 감지
    [SerializeField] private float attackRadius = 0.8f; // 공격 감지 범위
    private Transform target; // 타겟

    [Header("복제 정보")]
    private int direction = 1; // 방향
    private bool canDuplicate; // 복제 가능 여부
    private float duplicateChance; // 복제 확률

    private void Awake()
    {
        // 컴포넌트 가져오기
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime; // 클론 타이머 감소

        if (cloneTimer < 0) // 클론 지속시간 종료
        {
            // 클론 점점 투명화
            sr.color = new Color(1, 1, 1, sr.color.a - disappearSpeed * Time.deltaTime);

            if (sr.color.a <= 0) // 클론 완전 투명화
            {
                Destroy(gameObject); // 클론 제거
            }
        }
    }
    
    // 클론 설정 함수
    //public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration)
    //public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration, Vector3 _offest)
    public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration,
        //Vector3 _offest, Transform _target)
        Vector3 _offest, Transform _target, bool _canDuplicate, float _duplicateChance)
    {
        if (_canAttack) // 공격 가능
        {
            anim.SetInteger("Attack", Random.Range(1, 4)); // 클론 애니메이션 변경 (랜덤)
        }

        //transform.position = _transform.position; // 클론 위치
        transform.position = _transform.position + _offest; // 클론 위치 + 오프셋 적용
        cloneTimer = _cloneDuration; // 클론 타이머 초기화 = 클론 지속시간

        target = _target; // 현재 타겟
        FaceTarget(); // 타겟 추적

        canDuplicate = _canDuplicate;
        duplicateChance = _duplicateChance;
    }

    // 타겟 추적 함수 : 가장 가까운 타겟 찾기 + 타겟으로 방향 전환
    private void FaceTarget()
    {
        // 타겟으로 방향 전환
        if (target != null)
        {
            if (transform.position.x > target.position.x) // 클론이 타겟의 왼쪽에 있음
            {
                // 클론 방향 전환
                direction = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    // 애니메이션 트리거 함수 이벤트
    private void AnimationTrigger()
    {
        cloneTimer = -0.1f; // 클론 타이머 종료
    }

    // 공격 트리거 함수 이벤트
    private void AttackTrigger()
    {
        // 콜라이더 형성 = 플레이어 공격 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // 적과 접촉
            {
                // 적 데미지 이펙트
                hit.GetComponent<Enemy>().DamageEffect();
            }

            if (canDuplicate) // 복제 가능
            {
                if (Random.Range(0, 100) < duplicateChance) // 복제 확률
                {
                    // 클론 생성
                    SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(0.5f * direction, 0));
                }
            }
        }
    }
}