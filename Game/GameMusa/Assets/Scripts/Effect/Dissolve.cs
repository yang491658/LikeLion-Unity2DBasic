using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float dissolveTime = 0.75f; // 디졸브 시간

    // 컴포넌트
    private SpriteRenderer sr;
    private Material mat;

    // 셰이더 프로퍼티
    private int dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int verticalDissolve = Shader.PropertyToID("_VerticalDissolve");

    private void Start()
    {
        // 컴포넌트 가져오기
        sr = GetComponentInChildren<SpriteRenderer>();
        mat = sr.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) // 숫자 9키 입력
        {
            // 사라짐 코루틴
            StartCoroutine(Vanish(true, false));
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) // 숫자 0키 입력
        {
            // 나타남 코루틴
            StartCoroutine(Appear(true, false));
        }
    }

    // 사라짐 코루틴 : 디졸브를 통해 오브젝트가 사라짐
    private IEnumerator Vanish(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            // 시간에 따른 선형 보간 : 디졸브 효과 증가
            float lerpedDissolve = Mathf.Lerp(0, 1, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / dissolveTime));

            // 선택적 디졸브 효과 적용
            if (useDissolve) mat.SetFloat(dissolveAmount, lerpedDissolve);
            // 선택적 수직 디졸브 효과 적용
            if (useVertical) mat.SetFloat(verticalDissolve, lerpedVerticalDissolve);

            yield return null;
        }
    }

    // 나타남 코루틴 : 디졸브를 통해 오브젝트가 나타남
    private IEnumerator Appear(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            // 시간에 따른 선형 보간 : 디졸브 효과 감소
            float lerpedDissolve = Mathf.Lerp(1, 0, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(1.1f, 0, (elapsedTime / dissolveTime));

            // 선택적 디졸브 효과 적용
            if (useDissolve) mat.SetFloat(dissolveAmount, lerpedDissolve);
            // 선택적 수직 디졸브 효과 적용
            if (useVertical) mat.SetFloat(verticalDissolve, lerpedVerticalDissolve);

            yield return null;
        }
    }
}