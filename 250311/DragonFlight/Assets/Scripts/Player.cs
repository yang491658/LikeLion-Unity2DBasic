using UnityEngine;

public class Player : MonoBehaviour
{
    // 속도 정의
    public float speed = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 지정한 Axis를 통해 키의 방향을 판단
        // 속도와 프레임을 보정하여 이동량 결정
        // 움직이는 거리 = 방향 * 속도 * 시간(프레임 보정)
        float distanceX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // 이동량만큼 실제 이동 함수
        transform.Translate(distanceX, 0, 0);
    }
}
