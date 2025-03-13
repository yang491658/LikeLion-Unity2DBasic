using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경 스크롤 속도
    public float speed = 0.5f;
    // 쿼드의 등록된 머티리얼 데이터를 저장할 변수
    private Material material;

    void Start()
    {
        // 현재 객체의 컴포넌트 참조 -> 렌더러 컴포넌트의 머터리얼 정보
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Offset 벡터
        Vector2 offset = material.mainTextureOffset;
        // 현재 오프셋 + 스피드 * 시간(프레임 보정)
        offset.Set(0, offset.y + (speed * Time.deltaTime));
        // 최종 오프셋 값 지정
        material.mainTextureOffset = offset;
    }
}
