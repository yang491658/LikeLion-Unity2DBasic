using UnityEngine;

public class ShockController : MonoBehaviour
{
    private bool trigger; // 트리거 발동 여부

    private CharacterStats target; // 타겟
    private int damage; // 데미지

    private void Update()
    {
        if (!target) return; // 타겟 없을 시 종료

        if (trigger) // 트리거 발동
        {
            // 타겟 감전 적용
            target.ApplyShock(true);

            // 타겟 데미지 피격
            target.TakeDamage(damage);

            Destroy(gameObject); // 감전 제거
        }
    }

    // 감전 설정 함수
    public void SetShock(CharacterStats _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    // 애니메이션 트리거 함수 이벤트
    public virtual void AnimationTrigger() => trigger = true;
}
