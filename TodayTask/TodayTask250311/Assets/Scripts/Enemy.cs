using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도
    public float speed = 2.0f;

    void Start()
    {

    }

    void Update()
    {
        // 이동 거리 = 속도 * 시간(프레임 판정)
        float distanceY = speed * Time.deltaTime;
        // 아래 이동
        transform.Translate(0, -distanceY, 0);
    }

    // 화면을 벗어나면 제거 ~ 화면 : Game + Scene
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // 객체 삭제
    }
}
