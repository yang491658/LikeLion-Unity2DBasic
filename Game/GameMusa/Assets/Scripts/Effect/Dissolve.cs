using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float dissolveTime = 0.75f; // ������ �ð�

    // ������Ʈ
    private SpriteRenderer sr;
    private Material mat;

    // ���̴� ������Ƽ
    private int dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int verticalDissolve = Shader.PropertyToID("_VerticalDissolve");

    private void Start()
    {
        // ������Ʈ ��������
        sr = GetComponentInChildren<SpriteRenderer>();
        mat = sr.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) // ���� 9Ű �Է�
        {
            // ����� �ڷ�ƾ
            StartCoroutine(Vanish(true, false));
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) // ���� 0Ű �Է�
        {
            // ��Ÿ�� �ڷ�ƾ
            StartCoroutine(Appear(true, false));
        }
    }

    // ����� �ڷ�ƾ : �����긦 ���� ������Ʈ�� �����
    private IEnumerator Vanish(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            // �ð��� ���� ���� ���� : ������ ȿ�� ����
            float lerpedDissolve = Mathf.Lerp(0, 1, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / dissolveTime));

            // ������ ������ ȿ�� ����
            if (useDissolve) mat.SetFloat(dissolveAmount, lerpedDissolve);
            // ������ ���� ������ ȿ�� ����
            if (useVertical) mat.SetFloat(verticalDissolve, lerpedVerticalDissolve);

            yield return null;
        }
    }

    // ��Ÿ�� �ڷ�ƾ : �����긦 ���� ������Ʈ�� ��Ÿ��
    private IEnumerator Appear(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            // �ð��� ���� ���� ���� : ������ ȿ�� ����
            float lerpedDissolve = Mathf.Lerp(1, 0, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(1.1f, 0, (elapsedTime / dissolveTime));

            // ������ ������ ȿ�� ����
            if (useDissolve) mat.SetFloat(dissolveAmount, lerpedDissolve);
            // ������ ���� ������ ȿ�� ����
            if (useVertical) mat.SetFloat(verticalDissolve, lerpedVerticalDissolve);

            yield return null;
        }
    }
}