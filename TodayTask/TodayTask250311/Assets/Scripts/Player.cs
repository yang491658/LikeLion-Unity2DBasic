using UnityEngine;

public class Player : MonoBehaviour
{
    // 속도
    public float speed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        MoveX();
    }

    void MoveX()
    {
        // 이동 거리 = 방향(입력한 키) * 속도 * 시간(프레임 판정)
        float distanceX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // 좌우 이동
        transform.Translate(distanceX, 0, 0);
    }
}
