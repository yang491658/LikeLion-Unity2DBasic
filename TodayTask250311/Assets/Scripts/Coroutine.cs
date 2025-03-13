using System.Collections;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine("ExampleCoroutine");
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //Debug.Log("코루틴 시작");
        //yield return new WaitForSeconds(2f); // 2초 대기
        //Debug.Log("2초 후 실행");

        while (true)
        {
            Debug.Log("매 1초 마다 실행");
            yield return new WaitForSeconds(1f); // 1초 대기
        }
    }

    void Update()
    {

    }
}
