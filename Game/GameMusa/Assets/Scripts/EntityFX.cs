using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("�÷��� ����")]
    [SerializeField] private float flashDuration; // ������ ���ӽð�
    [SerializeField] private Material hitMat; // �ǰ� ���͸���
    private Material originalMat; // ���� ���͸���

    [Header("�����̻� ����")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        originalMat = sr.material; // ���� ��Ƽ���� ����
    }

    // ������ �ڷ�ƾ
    private IEnumerator Blink()
    {
        sr.material = hitMat; // ���͸��� ����
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat; // ���͸��� ���󺹱�
    }

    // ���� ������ �Լ�
    private void BlinkRed()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    // ������ ���� �Լ�
    private void CancelBlink()
    {
        CancelInvoke(); // �Լ� ����

        sr.color = Color.white; // ���� ���󺹱�
    }

    // ��ȭ Ư��ȿ�� �Լ�
    public void IgniteFX(float _seconds)
    {
        InvokeRepeating("IgniteBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // �ð� Ư��ȿ�� �Լ�
    public void ChillFX(float _seconds)
    {
        InvokeRepeating("ChillBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // ���� Ư��ȿ�� �Լ�
    public void ShockFX(float _seconds)
    {
        InvokeRepeating("ShockBlink", 0, 0.3f);
        Invoke("CancelBlink", _seconds);
    }

    // ��ȭ ������ �Լ�
    private void IgniteBlink()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }

    // �ð� ������ �Լ�
    private void ChillBlink()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }

    // ���� ������ �Լ�
    private void ShockBlink()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
}