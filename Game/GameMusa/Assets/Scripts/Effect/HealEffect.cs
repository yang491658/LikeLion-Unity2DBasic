using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Data/Effect/Heal")]
public class HealEffect : ItemEffect
{
    [Range(0, 100)][SerializeField] private float healPercent; // 힐 퍼센티지

    // 효과 실행 함수 (상속)
    public override void DoEffect(Transform _enemy)
    {
        // 플레이어 스탯
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        // 힐량 = 플레이어 최대 체력 x 힐 퍼센티지
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealth() * healPercent / 100);

        // 플레이어 체력 증가
        playerStats.IncreaseHealth(healAmount);
    }
}
