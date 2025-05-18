using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("플래시 정보")]
    [SerializeField] private float flashDuration; // 깜빡임 지속시간
    [SerializeField] private Material hitMat; // 피격 머터리얼
    private Material originalMat; // 원본 머터리얼

    [Header("상태이상 색상")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

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
        sr.material = originalMat; // 머터리얼 원상복구
    }

    // 빨강 깜빡임 함수
    private void BlinkRed()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    // 깜빡임 종료 함수
    private void CancelBlink()
    {
        CancelInvoke(); // 함수 종료

        sr.color = Color.white; // 색상 원상복구
    }

    // 점화 특수효과 함수
    public void IgniteFX(float _seconds)
    {
        InvokeRepeating("IgniteBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // 냉각 특수효과 함수
    public void ChillFX(float _seconds)
    {
        InvokeRepeating("ChillBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // 감전 특수효과 함수
    public void ShockFX(float _seconds)
    {
        InvokeRepeating("ShockBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // 점화 깜빡임 함수
    private void IgniteBlink()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }

    // 냉각 깜빡임 함수
    private void ChillBlink()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }

    // 감전 깜빡임 함수
    private void ShockBlink()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
}