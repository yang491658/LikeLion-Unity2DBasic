using UnityEngine;

public class Singleton : MonoBehaviour
{
    // ����Ƽ���� �̱����� ����ϸ� �ϳ��� �ν��Ͻ��� �����ϸ鼭 ��𼭵� ���� �����ϰ� ���� �� ����
    public static Singleton instance { get; private set; }

    // �Լ� �ѹ� ���� + start���� �� ���� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene�� �ٲ� �����ǰ��ϴ� �Լ�
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
}
