using UnityEngine;

public class ShockController : MonoBehaviour
{
    private bool trigger; // Ʈ���� �ߵ� ����

    private CharacterStats target; // Ÿ��
    private int damage; // ������

    private void Update()
    {
        if (!target) return; // Ÿ�� ���� �� ����

        if (trigger) // Ʈ���� �ߵ�
        {
            // Ÿ�� ���� ����
            target.ApplyShock(true);

            // Ÿ�� ������ �ǰ�
            target.TakeDamage(damage);

            Destroy(gameObject); // ���� ����
        }
    }

    // ���� ���� �Լ�
    public void SetShock(CharacterStats _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    // �ִϸ��̼� Ʈ���� �Լ� �̺�Ʈ
    public virtual void AnimationTrigger() => trigger = true;
}
