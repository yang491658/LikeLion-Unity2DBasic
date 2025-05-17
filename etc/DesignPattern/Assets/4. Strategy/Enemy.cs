using UnityEngine;

// 이동 전략 인터페이스
public interface IMovementStrategy
{
    void Move(Transform transform, float speed);
}

// 직선 이동 전략
public class StraightMovement : IMovementStrategy
{
    public void Move(Transform transform, float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

// 지그재그 이동 전략
public class ZigzagMovement : IMovementStrategy
{
    private float _amplitude = 2f;
    private float _frequency = 2f;
    private float _time = 0;

    public void Move(Transform transform, float speed)
    {
        _time += Time.deltaTime;

        // 직선 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 좌우 움직임 추가
        float xOffset = Mathf.Sin(_time * _frequency) * _amplitude;
        transform.position = new Vector3(xOffset, transform.position.y, transform.position.z);
    }
}

// 원형 이동 전략
public class CircularMovement : IMovementStrategy
{
    private float _radius = 5f;
    private float _angularSpeed = 50f;
    private float _angle = 0;
    private Vector3 _center;
    private bool _isInitialized = false;

    public void Move(Transform transform, float speed)
    {
        if (!_isInitialized)
        {
            _center = transform.position;
            _isInitialized = true;
        }

        _angle += _angularSpeed * Time.deltaTime;

        float x = _center.x + Mathf.Cos(_angle * Mathf.Deg2Rad) * _radius;
        float z = _center.z + Mathf.Sin(_angle * Mathf.Deg2Rad) * _radius;
        transform.position = new Vector3(x, transform.position.y, z);

        // 회전 방향 고려
        transform.LookAt(new Vector3(
            _center.x + Mathf.Cos((_angle + 90) * Mathf.Deg2Rad) * _radius,
            transform.position.y,
            _center.z + Mathf.Sin((_angle + 90) * Mathf.Deg2Rad) * _radius
            ));
    }
}

// 전략 사용 클래스
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    // 현재 이동 전략
    private IMovementStrategy _movementStrategy;

    private void Start()
    {
        // 기본 이동 전략
        _movementStrategy = new StraightMovement();
    }

    // 이동 전략 변경 메서드
    public void SetMovementStrategy(IMovementStrategy strategy)
    {
        _movementStrategy = strategy;
    }

    private void Update()
    {
        if (_movementStrategy != null)
        {
            _movementStrategy.Move(transform, moveSpeed);
        }
    }
}