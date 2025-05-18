using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>(); // 플레이어

    // 애니메이션 트리거 함수 이벤트
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // 공격 트리거 함수 이벤트
    private void AttackTrigger()
    {
        // 콜라이더 형성 = 플레이어 공격 범위
        Collider2D[] colliders
            = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // 적과 접촉
            {
                // 적 데미지 이펙트
                //hit.GetComponent<Enemy>().DamageEffect();

                // 적 데미지 피격
                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage);
                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());

                // 플레이어 데미지 공격
                player.stats.DoDmage(hit.GetComponent<EnemyStats>());

                // 플레이어 무기
                EquipmentData weapon = Inventory.instance.GetEquipmentType(EquipmentType.Weapon);
                if (weapon != null) // 플레이어 무기 있음
                {
                    weapon.DoItemEffect(hit.transform); // 무기 아이템 효과 실행
                }
            }
        }
    }

    // 던지기 함수 이벤트
    private void Throw()
    {
        // 소드 생성
        SkillManager.instance.sword.CreateSword();
    }
}