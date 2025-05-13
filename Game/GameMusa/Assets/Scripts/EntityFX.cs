using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("플래시 정보")]
    [SerializeField] private float flashDuration; // 깜빡임 지속시간
    [SerializeField] private Material hitMat; // 피격 머터리얼
    private Material originalMat; // 원본 머터리얼

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalMat = sr.material; // 원본 머티리얼 저장
    }

    // 깜빡임 코루틴
    private IEnumerator Blink()
    {
        sr.material = hitMat; // 머터리얼 변경
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat; // 머터리얼 원상 복구
    }

    // 깜빡임 (빨강) 함수
    private void RedBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    // 깜빡임 종료 함수
    private void CancelRedBlink()
    {
        CancelInvoke(); // 함수 종료

        sr.color = Color.white;
    }
}