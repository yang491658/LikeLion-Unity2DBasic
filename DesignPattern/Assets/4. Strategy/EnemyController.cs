using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        // 키 입력에 따라 이동 전략 변경 (테스트용)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _enemy.SetMovementStrategy(new StraightMovement());
            Debug.Log("직선 이동 전략으로 변경");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _enemy.SetMovementStrategy(new ZigzagMovement());
            Debug.Log("지그재그 이동 전략으로 변경");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _enemy.SetMovementStrategy(new CircularMovement());
            Debug.Log("원형 이동 전략으로 변경");
        }
    }
}