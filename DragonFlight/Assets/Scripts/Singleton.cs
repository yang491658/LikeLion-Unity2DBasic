using UnityEngine;

public class Singleton : MonoBehaviour
{
    // 유니티에서 싱글톤을 사용하면 하나의 인스턴스만 유지하면서 어디서든 접근 가능하게 만들 수 있음
    public static Singleton instance { get; private set; }

    // 함수 한번 실행 + start보다 더 빠른 실행
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene이 바뀌어도 유지되게하는 함수
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    public void PrintMessage()
    {
        Debug.Log("싱글톤 메시지 출력");
    }
}
