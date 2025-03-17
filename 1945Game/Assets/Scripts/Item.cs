using UnityEngine;

public class Item : MonoBehaviour
{
    // 속도
    public float speed = 100f;
    // 리지드바디 변수
    Rigidbody2D rig = null;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(speed, speed, 0f));
    }
}
