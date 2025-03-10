using UnityEngine;

public class FunctionExample : MonoBehaviour
{
    // 함수 정의
    void SayHello()
    {
        Debug.Log("Hello, Unity!");
    }

    int AddNumbers(int a, int b)
    {
        return a + b;
    }

    void Start()
    {
        SayHello(); // 함수 호출
        int total = AddNumbers(3, 5);
        Debug.Log("Total : " + total);
    }
}
