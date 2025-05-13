using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player; // �÷��̾�

    [SerializeField] protected float cooldown; // ��ٿ�
    protected float cooldownTimer; // ��ٿ� Ÿ�̸�

    protected virtual void Start()
    {
        player = PlayerManager.instance.player; // �÷��̾� ã��
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime; // ��Ÿ�� Ÿ�̸� ����
    }

    // ��ų ��� ���� �Լ�
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0) // ��ٿ� ����
        {
            cooldownTimer = cooldown; // ��ٿ� Ÿ�̸� �ʱ�ȭ = ��ٿ�

            UseSkill(); // ��ų ���

            return true;
        }

        return false;
    }

    // ��ų ��� �Լ�
    public virtual void UseSkill()
    {
    }

    // Ÿ�� ã�� �Լ� : ���� ����� Ÿ�� ã��
    protected virtual Transform FindTarget(Transform _transform)
    {
        // �ݶ��̴� ���� = Ÿ�� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 25);

        float distance = Mathf.Infinity; // �Ÿ� �ʱ�ȭ = ���Ѵ�

        Transform target = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // ���� ����
            {
                float distanceToEnemy
                    = Vector2.Distance(_transform.position, hit.transform.position); // ������ �Ÿ� ���

                if (distanceToEnemy < distance) // ���� Ÿ�ٺ��� �� �����
                {
                    distance = distanceToEnemy; // �Ÿ� ����
                    target = hit.transform; // ���� Ÿ�� ����
                }
            }
        }

        return target;
    }
}