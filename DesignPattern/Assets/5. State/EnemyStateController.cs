using UnityEngine;
using UnityEngine.AI;

// 적 상태 인터페이스
public interface IEnemyState
{
    void EnterState(EnemyStateController enemy);
    void UpdateState(EnemyStateController enemy);
    void ExitState(EnemyStateController enemy);
    void OnTriggerState(EnemyStateController enemy, Collider other);
}

// 순찰 상태
public class PatrolState : IEnemyState
{
    private float _patrolTimer = 0f;
    private int _currentWaypointIndex = 0;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("순찰 상태 시작");

        // 첫 번째 웨이포인트로 이동
        if (enemy.waypoints.Length > 0)
        {
            enemy.navMeshAgent.SetDestination(enemy.waypoints[0].position);
        }
    }

    public void UpdateState(EnemyStateController enemy)
    {
        _patrolTimer += Time.deltaTime;

        // 플레이어가 시야에 들어왔는지 확인
        if (CanSeePlayer(enemy))
        {
            enemy.TransitionToState(new ChaseState());
            return;
        }

        // 현재 웨이포인트에 도착했는지 확인
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance < 0.5f)
        {
            // 다음 웨이포인트로 순환
            _currentWaypointIndex = (_currentWaypointIndex + 1) % enemy.waypoints.Length;
            enemy.navMeshAgent.SetDestination(enemy.waypoints[_currentWaypointIndex].position);
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("순찰 상태 종료");
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // 트리거 이벤트 처리
        if (other.CompareTag("Player"))
        {
            enemy.TransitionToState(new ChaseState());
        }
    }
    private bool CanSeePlayer(EnemyStateController enemy)
    {
        if (enemy.player == null) return false;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer <= enemy.detectionRange)
        {
            Vector3 directionToPlayer = (enemy.player.position - enemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

            if (angleToPlayer <= enemy.fieldOfView / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, enemy.detectionRange))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

// 추적 상태
public class ChaseState : IEnemyState
{
    private float _lastKnownPlayerPositionTimer = 0f;
    private Vector3 _lastKnownPlayerPosition;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("추적 상태 시작");
        enemy.navMeshAgent.speed = enemy.chaseSpeed;

        if (enemy.player != null)
        {
            _lastKnownPlayerPosition = enemy.player.position;
            enemy.navMeshAgent.SetDestination(_lastKnownPlayerPosition);
        }
    }

    public void UpdateState(EnemyStateController enemy)
    {
        if (enemy.player == null)
        {
            // 플레이어가 없으면 마지막 위치로 이동 후 순찰 상태로 돌아감
            _lastKnownPlayerPositionTimer += Time.deltaTime;
            if (_lastKnownPlayerPositionTimer > enemy.memoryDuration)
            {
                enemy.TransitionToState(new PatrolState());
                return;
            }
        }
        else
        {
            // 플레이어가 있으면 계속 추적
            _lastKnownPlayerPosition = enemy.player.position;
            _lastKnownPlayerPositionTimer = 0f;
            enemy.navMeshAgent.SetDestination(_lastKnownPlayerPosition);

            // 공격 범위 내에 들어왔는지 확인
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.TransitionToState(new AttackState());
                return;
            }
        }

        // 플레이어를 놓쳤는지 확인
        if (!CanSeePlayer(enemy) && _lastKnownPlayerPositionTimer > enemy.memoryDuration / 2)
        {
            enemy.TransitionToState(new SearchState());
            return;
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("추적 상태 종료");
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // 필요한 트리거 이벤트 처리
    }

    private bool CanSeePlayer(EnemyStateController enemy)
    {
        if (enemy.player == null) return false;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer <= enemy.detectionRange)
        {
            Vector3 directionToPlayer = (enemy.player.position - enemy.transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, enemy.detectionRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}

// 공격 상태
public class AttackState : IEnemyState
{
    private float _attackTimer = 0f;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("공격 상태 시작");
        enemy.navMeshAgent.isStopped = true;
        _attackTimer = 0f;
    }

    public void UpdateState(EnemyStateController enemy)
    {
        if (enemy.player == null)
        {
            enemy.TransitionToState(new PatrolState());
            return;
        }

        // 플레이어를 바라보기
        enemy.transform.LookAt(new Vector3(enemy.player.position.x, enemy.
       transform.position.y, enemy.player.position.z));
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= enemy.attackCooldown)
        {
            Attack(enemy);
            _attackTimer = 0f;
        }

        // 공격 범위 밖으로 나가면 다시 추적
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.TransitionToState(new ChaseState());
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("공격 상태 종료");
        enemy.navMeshAgent.isStopped = false;
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // 필요한 트리거 이벤트 처리
    }

    private void Attack(EnemyStateController enemy)
    {
        Debug.Log("적이 공격합니다!");
        // 여기에 적의 공격 로직을 구현
        // 예: 애니메이션 재생, 데미지 처리 등
    }
}

// 수색 상태
public class SearchState : IEnemyState
{
    private float _searchTimer = 0f;
    private Vector3 _lastKnownPlayerPosition;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("수색 상태 시작");
        _searchTimer = 0f;
        _lastKnownPlayerPosition = enemy.player != null ?
        enemy.player.position : enemy.transform.position;

        // 마지막 플레이어 위치 주변을 수색
        enemy.navMeshAgent.SetDestination(_lastKnownPlayerPosition);
    }

    public void UpdateState(EnemyStateController enemy)
    {
        _searchTimer += Time.deltaTime;
        // 플레이어를 다시 발견했는지 확인
        if (CanSeePlayer(enemy))
        {
            enemy.TransitionToState(new ChaseState());
            return;
        }

        // 일정 시간 동안 찾지 못하면 순찰 상태로 전환
        if (_searchTimer >= enemy.searchDuration)
        {
            enemy.TransitionToState(new PatrolState());
            return;
        }

        // 목적지에 도착했지만 플레이어를 발견하지 못한 경우, 주변을 랜덤하게 수색
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f;
            randomDirection += _lastKnownPlayerPosition;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
            {
                enemy.navMeshAgent.SetDestination(hit.position);
            }
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("수색 상태 종료");
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.TransitionToState(new ChaseState());
        }
    }

    private bool CanSeePlayer(EnemyStateController enemy)
    {
        if (enemy.player == null) return false;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distanceToPlayer <= enemy.detectionRange)
        {
            Vector3 directionToPlayer = (enemy.player.position - enemy.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

            if (angleToPlayer <= enemy.fieldOfView / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, enemy.detectionRange))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

// 적 상태 컨트롤러
public class EnemyStateController : MonoBehaviour
{
    [Header("Navigation")]
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    [Header("Detection")]
    public Transform player;
    public float detectionRange = 10f;
    public float fieldOfView = 90f;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Attack")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("AI Parameters")]
    public float memoryDuration = 5f;
    public float searchDuration = 8f;

    // 현재 상태
    private IEnemyState _currentState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
    }

    private void Start()
    {
        // 기본 상태로 순찰 설정
        TransitionToState(new PatrolState());
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentState != null)
        {
            _currentState.OnTriggerState(this, other);
        }
    }

    // 상태 전환 메서드
    public void TransitionToState(IEnemyState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState(this);
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.EnterState(this);
        }
    }
}