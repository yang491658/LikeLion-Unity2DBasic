using UnityEngine;

public class Singleton : MonoBehaviour
{
    // �̱��� ��� -> �ϳ��� �ν��Ͻ��� ���� -> ��𼭵� ���� ����
    public static Singleton instance { get; private set; }

    // �Լ� ���� 1ȸ���� , start���� �� ���� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene�� �ٲ� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ���� ����
        }
    }

    public void PrintMessage()
    {
        Debug.Log("�̱��� �޽��� ���");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
