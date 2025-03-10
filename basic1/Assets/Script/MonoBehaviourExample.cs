using UnityEngine;

public class MonoBehaviourExample : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start : 게임이 시작될 때 호출됩니다.");
    }

    void Update()
    {
        Debug.Log("Update : 프레임마다 호출됩니다.");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate : 물리 연산에 사용됩니다.");
    }

    // 컨트롤 + 쉬프트 + m
}
