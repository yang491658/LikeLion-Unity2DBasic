using UnityEngine;

public class EffectController : MonoBehaviour
{
    // 트리거 충돌 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // 적과 접촉
        {
            PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();
            EnemyStats enemy = collision.GetComponent<EnemyStats>();

            // 플레이어 마법 데미지
            player.DoMagicalDamage(enemy);
        }
    }
}
