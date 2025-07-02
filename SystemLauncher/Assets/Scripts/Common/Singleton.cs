using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected bool m_IsDestroyOnLoad = false; // 씬 전환 시 삭제 여부

    // 싱글톤
    protected static T instance; // 인스턴스
    public static T Instance // 프로퍼티
    {
        get { return instance; }
    }

    private void Awake()
    {
        Init(); // 초기화
    }

    // 초기화 함수
    protected virtual void Init()
    {
        // 싱글톤 초기화
        if (instance == null) // 인스턴스 없음
        {
            instance = (T)this; // 인스턴스 초기화

            if (!m_IsDestroyOnLoad) // 씬 전환 시 유지
            {
                DontDestroyOnLoad(this); // 오브젝트 유지
            }
        }
        else // 인스턴스 있음
        {
            Destroy(gameObject); // 오브젝트 제거
        }
    }

    // 파괴 시 호출 함수
    protected virtual void OnDestroy()
    {
        Dispose(); // 폐기
    }

    // 폐기 함수
    protected virtual void Dispose()
    {
        instance = null; // 인스턴스 제거
    }
}