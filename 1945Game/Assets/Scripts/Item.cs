using UnityEngine;

public class Item : MonoBehaviour
{
    // �ӵ�
    public float speed = 100f;
    // ������ٵ� ����
    Rigidbody2D rig = null;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(speed, speed, 0f));
    }
}
