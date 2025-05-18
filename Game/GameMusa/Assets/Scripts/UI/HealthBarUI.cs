using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity; // ��ƼƼ
    private CharacterStats stats; // ĳ���� ����
    private RectTransform recTransform; // UI Ʈ������
    private Slider slider; // �����̴�

    private void Start()
    {
        // ������Ʈ ��������
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<CharacterStats>();
        recTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();

        // �޼��� �߰�
        entity.onFlip += FlipUI; // ���� ��ȯ ��������Ʈ = UI ���� ��ȯ �޼��� �߰�
        stats.onChangeHealth += UpdateHealthUI; // ü�� ���� ��������Ʈ = ü�� UI ������Ʈ �޼��� �߰�
    }

    // UI ���� ��ȯ �Լ�
    public void FlipUI() => recTransform.Rotate(0, 180, 0);

    private void Update()
    {
        UpdateHealthUI(); // ü�� UI ������Ʈ
    }

    // ü�� UI ������Ʈ �Լ�
    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealth(); // �ִ� ü�� ����
        slider.value = stats.currentHealth; // ���� ü�� ����
    }

    // ��Ȱ��ȭ �Լ�
    private void OnDisable()
    {
        entity.onFlip -= FlipUI; // ���� ��ȯ ��������Ʈ = UI ���� ��ȯ �޼��� ����
        stats.onChangeHealth -= UpdateHealthUI; // ü�� ���� ��������Ʈ = ü�� UI ������Ʈ �޼��� ����
    }
}
