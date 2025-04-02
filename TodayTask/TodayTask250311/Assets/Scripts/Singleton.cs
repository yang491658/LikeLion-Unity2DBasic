using UnityEngine;

public class Singleton : MonoBehaviour
{
    // 싱글톤 사용 -> 하나의 인스턴스만 유지 -> 어디서든 접근 가능
    public static Singleton instance { get; private set; }

    // 함수 최초 1회실행 , start보다 더 빠른 실행
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene이 바뀌어도 유지
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

    void Start()
    {

    }

    void Update()
    {

    }
}
