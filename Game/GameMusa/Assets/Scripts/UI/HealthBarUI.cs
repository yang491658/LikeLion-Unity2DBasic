using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity; // 엔티티
    private CharacterStats stats; // 캐릭터 스탯
    private RectTransform recTransform; // UI 트랜스폼
    private Slider slider; // 슬라이더

    private void Start()
    {
        // 컴포넌트 가져오기
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<CharacterStats>();
        recTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();

        // 메서드 추가
        entity.onFlip += FlipUI; // 방향 전환 델리게이트 = UI 방향 전환 메서드 추가
        stats.onChangeHealth += UpdateHealthUI; // 체력 변경 델리게이트 = 체력 UI 업데이트 메서드 추가
    }

    // UI 방향 전환 함수
    public void FlipUI() => recTransform.Rotate(0, 180, 0);

    private void Update()
    {
        UpdateHealthUI(); // 체력 UI 업데이트
    }

    // 체력 UI 업데이트 함수
    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealth(); // 최대 체력 설정
        slider.value = stats.currentHealth; // 현재 체력 설정
    }

    // 비활성화 함수
    private void OnDisable()
    {
        entity.onFlip -= FlipUI; // 방향 전환 델리게이트 = UI 방향 전환 메서드 제거
        stats.onChangeHealth -= UpdateHealthUI; // 체력 변경 델리게이트 = 체력 UI 업데이트 메서드 제거
    }
}
