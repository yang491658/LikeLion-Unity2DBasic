using UnityEngine;

public class MineralLight : MonoBehaviour
{
    public float speed = 5;  // 속도

    private Vector3 initialScale; // 초기 크기
    public float scaleFactor = 0.2f;  // 크기 변화량

    void Start()
    {
        initialScale = transform.localScale; // 크기 초기화
    }

    void Update()
    {
        float time = Time.time * speed; // 시간
        float sinValue = Mathf.Sin(time); // 사인 함수 적용
        float scaleChange = sinValue * scaleFactor; // 스케일 변화량 계산

        // 모든 축에 변화 적용
        transform.localScale = initialScale + new Vector3(scaleChange, scaleChange, scaleChange);
    }
}