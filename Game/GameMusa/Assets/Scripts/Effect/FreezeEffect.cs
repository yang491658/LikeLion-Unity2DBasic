using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffect", menuName = "Data/Effect/Freeze")]
public class FreezeEffect : ItemEffect
{
    [SerializeField] private float freezeDuration; // 동결 지속시간

    // 효과 실행 함수 (상속)
    public override void DoEffect(Transform _transform)
    {
        // 플레이어 스탯
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats.currentHealth >= playerStats.GetMaxHealth() * 0.1f ||
            // 플레이어 현재 체력 = 최대 체력의 10% 이상
            !Inventory.instance.UseArmor()) // 방어구 사용 실패
        {
            return; // 종료
        }

        // 콜라이더 형성 = 동결 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in colliders)
        {
            // 적 시간 정지
            hit.GetComponent<Enemy>()?.StartCoroutine("FreezeTimeCoruntine", freezeDuration);
        }
    }
}
