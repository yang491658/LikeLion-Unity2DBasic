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
        Debug.Log("Tank�� ������ ����� ������ �մϴ�.");
    }
}