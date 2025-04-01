using UnityEngine;

// 적 팩토리 클래스
public class EnemyFactory : MonoBehaviour
{
    // 싱글톤 패턴 적용
    private static EnemyFactory _instance;
    public static EnemyFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("EnemyFactory");
                _instance = go.AddComponent<EnemyFactory>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    // 프리팹 참조 (Inspector에서 할당)
    public GameObject grunt;
    public GameObject runner;
    public GameObject tank;
    public GameObject boss;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 적 생성 메서드
    public Enemy CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject enemyObject = null;

        // 적 타입에 따라 다른 프리팹 사용
        switch (type)
        {
            case EnemyType.Grunt:
                enemyObject = Instantiate(grunt);
                break;
            case EnemyType.Runner:
                enemyObject = Instantiate(runner);
                break;
            case EnemyType.Tank:
                enemyObject = Instantiate(tank);
                break;
            case EnemyType.Boss:
                enemyObject = Instantiate(boss);
                break;
            default:
                Debug.LogError($"Unknown enemy type: {type}");
                return null;
        }

        // 생성된 적 초기화
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.Initialize(position);
        return enemy;
    }
}