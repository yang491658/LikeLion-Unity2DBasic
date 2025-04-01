using UnityEngine;

// 구현 예시 (실제 적 유형)
public class Grunt : EnemyBase
{
    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        health = 50;
        speed = 3f;
        damage = 10f;
    }

    public override void Attack()
    {
        Debug.Log("Grunt이 근접 공격을 합니다.");
    }
}