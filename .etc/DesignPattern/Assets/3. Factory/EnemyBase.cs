using UnityEngine;

// 적 타입 열거형
public enum EnemyType
{
    Grunt,
    Runner,
    Tank,
    Boss
}

// 모든 적의 기본 인터페이스
public interface IEnemy
{
    void Initialize(Vector3 position);
    void Attack();
    void TakeDamage(float damage);
}

// 기본 적 클래스
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