using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도 정의
    public float speed = 2.0f;

    void Start()
    {

    }

    void Update()
    {
        // 움직이는 거리 계산
        float distanceY = speed * Time.deltaTime;
        // 움직임 반영
        transform.Translate(0, -distanceY, 0);
    }

    // 화면 밖으로 나갈 경우 (카메라에 보이지 않으면) 호출
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // 객체 삭제
    }
}
