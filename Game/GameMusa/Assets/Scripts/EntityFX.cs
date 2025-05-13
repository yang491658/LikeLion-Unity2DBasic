using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("�÷��� ����")]
    [SerializeField] private float flashDuration; // ������ ���ӽð�
    [SerializeField] private Material hitMat; // �ǰ� ���͸���
    private Material originalMat; // ���� ���͸���

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
        sr.material = originalMat; // ���͸��� ���� ����
    }

    // ������ (����) �Լ�
    private void RedBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    // ������ ���� �Լ�
    private void CancelRedBlink()
    {
        CancelInvoke(); // �Լ� ����

        sr.color = Color.white;
    }
}