using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>(); // �÷��̾�

    // �ִϸ��̼� Ʈ���� �Լ� �̺�Ʈ
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    // ���� Ʈ���� �Լ� �̺�Ʈ
    private void AttackTrigger()
    {
        // �ݶ��̴� ���� = �÷��̾� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // ���� ����
            {
                // �� ������ ����Ʈ
                //hit.GetComponent<Enemy>().DamageEffect();

                // �� ������ �ǰ�
                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage);
                //hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());

                // �÷��̾� ������ ����
                player.stats.DoDmage(hit.GetComponent<EnemyStats>());
            }
        }
    }

    // ������ �Լ� �̺�Ʈ
    private void Throw()
    {
        // �ҵ� ����
        SkillManager.instance.sword.CreateSword();
    }
}