using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player; // 플레이어

    [SerializeField] protected float cooldown; // 쿨다운
    protected float cooldownTimer; // 쿨다운 타이머

    protected virtual void Start()
    {
        player = PlayerManager.instance.player; // 플레이어 찾기
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime; // 쿨타운 타이머 감소
    }

    // 스킬 사용 가능 함수
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0) // 쿨다운 종료
        {
            cooldownTimer = cooldown; // 쿨다운 타이머 초기화 = 쿨다운

            UseSkill(); // 스킬 사용

            return true;
        }

        return false;
    }

    // 스킬 사용 함수
    public virtual void UseSkill()
    {
    }

    // 타겟 찾기 함수 : 가장 가까운 타겟 찾기
    protected virtual Transform FindTarget(Transform _transform)
    {
        // 콜라이더 형성 = 타겟 감지 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 25);

        float distance = Mathf.Infinity; // 거리 초기화 = 무한대

        Transform target = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // 적과 접촉
            {
                float distanceToEnemy
                    = Vector2.Distance(_transform.position, hit.transform.position); // 적과의 거리 계산

                if (distanceToEnemy < distance) // 현재 타겟보다 더 가까움
                {
                    distance = distanceToEnemy; // 거리 저장
                    target = hit.transform; // 현재 타겟 지정
                }
            }
        }

        return target;
    }
}