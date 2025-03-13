using UnityEngine;

public class LoopExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for 문 : 1부터 10까지 출력
        for (int i = 1; i <= 10; i++)
        {
            Debug.Log("Count : " + i);
        }

        // while 문 : 조건이 참일 때 실행
        int counter = 0;
        while (counter < 5)
        {
            Debug.Log("While Count : " + counter);
            counter++;
        }
    }
}
