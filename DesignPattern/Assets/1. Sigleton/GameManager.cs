using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 정적 변수
    private static GameManager _instance;

    // 외부에서 인스턴스에 접근할 수 있는 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 찾아보기
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                // 씬에서도 찾을 수 없으면 새로 생성
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");

                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    // 게임 시작 시 호출
    private void Awake()
    {
        // 이미 인스턴스가 있는지 확인
        if (_instance != null && _instance != this)
        {
            // 중복된 인스턴스는 제거
            Destroy(gameObject);
            return;
        }

        // 이 인스턴스를 싱글톤으로 설정
        _instance = this;

        // 씬 전환 시에도 유지
        DontDestroyOnLoad(gameObject);
    }

    // 게임 점수 관리 예시
    private int _score = 0;

    public int Score => _score;

    public void AddScore(int points)
    {
        _score += points;

        Debug.Log($"Score updated : {_score}");
    }
}