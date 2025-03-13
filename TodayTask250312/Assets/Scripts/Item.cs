using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    // �ӵ�
    public float speed = 100f;
    // ������ٵ� ����
    Rigidbody2D rig = null;
    // ���� ����
    public static bool exist;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(speed, speed, 0f));
    }

    void Update()
    {

    }

    // �÷��̾�� �浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PBullet.power = Math.Min(PBullet.power+1,4);

            // ����
            Destroy(gameObject);

            exist = false;
        }
    }
}
