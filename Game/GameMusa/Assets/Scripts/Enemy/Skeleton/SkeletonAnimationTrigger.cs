using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Skeleton enemy => GetComponentInParent<Skeleton>(); // �ذ�

    // �ִϸ��̼� Ʈ���� �Լ� �̺�Ʈ
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    // ���� Ʈ���� �Լ� �̺�Ʈ
    private void AttackTrigger()
    {
        // �ݶ��̴� ���� = �ذ� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null) // �÷��̾�� ����
            {
                // �÷��̾� ������ ����Ʈ
                //hit.GetComponent<Player>().DamageEffect();

                // �ذ� ������ ����
                enemy.stats.DoDmage(hit.GetComponent<PlayerStats>());
            }
        }
    }

    // �ݰ� Ÿ�̹� Ȱ��ȭ �Լ� �̺�Ʈ
    private void OpenCounter() => enemy.OpenCounterTime();

    // �ݰ� Ÿ�̹� ��ȭ��ȭ �Լ� �̺�Ʈ
    private void CloseCouter() => enemy.CloseCounterTime();
}