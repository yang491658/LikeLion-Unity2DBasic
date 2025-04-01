using UnityEngine;

public class Runner : EnemyBase
{
    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        health = 30;
        speed = 6f;
        damage = 5f;
    }

    public override void Attack()
    {
        Debug.Log("Runner가 빠르게 연속 공격을 합니다.");
    }
}