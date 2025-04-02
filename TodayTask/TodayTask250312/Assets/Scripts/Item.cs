using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 속도
    public float speed = 100f;
    // 리지드바디 변수
    Rigidbody2D rig = null;
    // 존재 유무
    public static bool exist;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(speed, speed, 0f));
    }

    void Update()
    {

    }

    // 플레이어와 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PBullet.power = Math.Min(PBullet.power+1,4);

            // 제거
            Destroy(gameObject);

            exist = false;
        }
    }
}
