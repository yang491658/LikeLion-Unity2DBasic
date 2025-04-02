using System.Collections;
using TMPro;
using UnityEngine;

public class TMPColor : MonoBehaviour
{
    // ���� ��ȯ�� �ɸ��� �ð�
    [SerializeField]
    float lerpTIme = 0.1f;

    // �ؽ�Ʈ ������Ʈ
    TextMeshProUGUI textBossWarning;

    // Awake �޼��� : ������Ʈ �ʱ�ȭ
    private void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    // OnEnbale �޼��� : ������Ʈ�� Ȱ��Ȱ�� �� ȣ��
    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    // ���� ��ȯ ���� �ڷ�ƾ
    IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    // ���� ��ȯ �ڷ�ƾ
    IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0;
        float percent = 0;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTIme;
            textBossWarning.color = Color.Lerp(startColor, endColor, percent);
            yield return null;
        }
    }
}
