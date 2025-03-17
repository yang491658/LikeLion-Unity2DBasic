using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    // 스크롤 속도
    public float scrollSpeed = 0.4f;
    // 쿼드의 머터리얼 데이터를 받아올 객체 선언
    private Material thisMaterial;

    void Start()
    {
        // 객체가 생성될 때, 최초 1회 호출되는 함수
        // 현재 객체의 Component들을 참조해 Renderer라는 컴포넌트의 Material 정보를 받아옴
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // 새롭게 지정해줄 Offest 객체 선언
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        // y 값 = 현재 y 좌표 + 속도 * 프레임 보정
        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime));
        // 최종적으로 오프셋 값 지정
        thisMaterial.mainTextureOffset = newOffset;
    }
}
