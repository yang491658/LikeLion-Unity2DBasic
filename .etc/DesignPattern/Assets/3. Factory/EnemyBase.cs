using UnityEngine;

// �� Ÿ�� ������
public enum EnemyType
{
    Grunt,
    Runner,
    Tank,
    Boss
}

// ��� ���� �⺻ �������̽�
public interface IEnemy
{
    void Initialize(Vector3 position);
    void Attack();
    void TakeDamage(float damage);
}

// �⺻ �� Ŭ����
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public float health;
    public float speed;
    public float damage;

    public virtual void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    public abstract void Attack();

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}