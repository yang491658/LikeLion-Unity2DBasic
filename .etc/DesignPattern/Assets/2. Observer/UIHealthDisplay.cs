using UnityEngine;

// �̺�Ʈ ������ (������)
public class UIHealthDisplay : MonoBehaviour
{
    void Start()
    {
        // �̺�Ʈ ����
        EventManager.Instance.AddListener("PlayerHealthChanged", OnPlayerHealthChanged);
        EventManager.Instance.AddListener("PlayerDied", OnPlayerDied);
    }

    private void OnDestroy()
    {
        // ���� ����
        EventManager.Instance.RemoveListener("PlayerHealthChanged", OnPlayerHealthChanged);
        EventManager.Instance.RemoveListener("PlayerDied", OnPlayerDied);
    }


    private void OnPlayerHealthChanged(object data)
    {
        int health = (int)data;
        Debug.Log($"UI ������Ʈ: �÷��̾� ü���� {health}�� ����Ǿ����ϴ�.");
        // �����δ� ���⼭ UI ��Ҹ� ������Ʈ�մϴ�
    }


    private void OnPlayerDied(object data)
    {
        Debug.Log("UI ������Ʈ: �÷��̾ ����߽��ϴ�!");
        // ���� ���� ȭ�� ǥ�� ���� ���� ����
    }
}