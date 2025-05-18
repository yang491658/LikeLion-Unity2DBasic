using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Skeleton enemy => GetComponentInParent<Skeleton>(); // 해골

    // 애니메이션 트리거 함수 이벤트
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    // 공격 트리거 함수 이벤트
    private void AttackTrigger()
    {
        // 콜라이더 형성 = 해골 공격 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null) // 플레이어와 접촉
            {
                // 플레이어 데미지 이펙트
                //hit.GetComponent<Player>().DamageEffect();

                // 해골 데미지 공격
                enemy.stats.DoDmage(hit.GetComponent<PlayerStats>());
            }
        }
    }

    // 반격 타이밍 활성화 함수 이벤트
    private void OpenCounter() => enemy.OpenCounterTime();

    // 반격 타이밍 비화성화 함수 이벤트
    private void CloseCouter() => enemy.CloseCounterTime();
}