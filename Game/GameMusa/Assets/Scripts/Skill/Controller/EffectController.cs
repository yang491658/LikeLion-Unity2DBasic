using UnityEngine;

public class EffectController : MonoBehaviour
{
    // Ʈ���� �浹 �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null) // ���� ����
        {
            PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();
            EnemyStats enemy = collision.GetComponent<EnemyStats>();

            // �÷��̾� ���� ������
            player.DoMagicalDamage(enemy);
        }
    }
}
