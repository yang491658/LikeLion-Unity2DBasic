using UnityEngine;

// �� ���丮 Ŭ����
public class EnemyFactory : MonoBehaviour
{
    // �̱��� ���� ����
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

    // ������ ���� (Inspector���� �Ҵ�)
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

    // �� ���� �޼���
    public Enemy CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject enemyObject = null;

        // �� Ÿ�Կ� ���� �ٸ� ������ ���
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

        // ������ �� �ʱ�ȭ
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.Initialize(position);
        return enemy;
    }
}