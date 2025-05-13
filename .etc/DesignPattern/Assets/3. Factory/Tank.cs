using UnityEngine;

public class Tank : EnemyBase
{
    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        health = 200;
        speed = 1.5f;
        damage = 25f;
    }

    public override void Attack()
    {
        Debug.Log("Tank가 강력한 충격파 공격을 합니다.");
    }
}