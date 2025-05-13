using UnityEngine;

public class Background : MonoBehaviour
{
    private GameObject cam; // 카메라

    // 패럴럭스 계수 (0에 가까울수록 느리게, 1에 가까울수록 카메라와 함께 이동)
    [SerializeField] private float parallaxEffect;

    private float xPosition; // 위치
    private float length; // 길이

    void Start()
    {
        cam = Camera.main.gameObject; // 메인 카메라

        xPosition = transform.position.x; // 배경이미지의 x 위치
        length = GetComponent<SpriteRenderer>().bounds.size.x; // 배경이미지의 가로 길이
    }

    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect; // 이동 거리

        // 배경 이동
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); // 이동 거리 (카메라 기준)

        // 무한 배경 : 배경의 시작점에서 일정 거리 벗어나면 배경 이동
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}