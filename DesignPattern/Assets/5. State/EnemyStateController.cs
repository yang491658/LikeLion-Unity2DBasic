using UnityEngine;
using UnityEngine.AI;

// �� ���� �������̽�
public interface IEnemyState
{
    void EnterState(EnemyStateController enemy);
    void UpdateState(EnemyStateController enemy);
    void ExitState(EnemyStateController enemy);
    void OnTriggerState(EnemyStateController enemy, Collider other);
}

// ���� ����
public class PatrolState : IEnemyState
{
    private float _patrolTimer = 0f;
    private int _currentWaypointIndex = 0;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");

        // ù ��° ��������Ʈ�� �̵�
        if (enemy.waypoints.Length > 0)
        {
            enemy.navMeshAgent.SetDestination(enemy.waypoints[0].position);
        }
    }

    public void UpdateState(EnemyStateController enemy)
    {
        _patrolTimer += Time.deltaTime;

        // �÷��̾ �þ߿� ���Դ��� Ȯ��
        if (CanSeePlayer(enemy))
        {
            enemy.TransitionToState(new ChaseState());
            return;
        }

        // ���� ��������Ʈ�� �����ߴ��� Ȯ��
        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance < 0.5f)
        {
            // ���� ��������Ʈ�� ��ȯ
            _currentWaypointIndex = (_currentWaypointIndex + 1) % enemy.waypoints.Length;
            enemy.navMeshAgent.SetDestination(enemy.waypoints[_currentWaypointIndex].position);
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // Ʈ���� �̺�Ʈ ó��
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

// ���� ����
public class ChaseState : IEnemyState
{
    private float _lastKnownPlayerPositionTimer = 0f;
    private Vector3 _lastKnownPlayerPosition;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
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
            // �÷��̾ ������ ������ ��ġ�� �̵� �� ���� ���·� ���ư�
            _lastKnownPlayerPositionTimer += Time.deltaTime;
            if (_lastKnownPlayerPositionTimer > enemy.memoryDuration)
            {
                enemy.TransitionToState(new PatrolState());
                return;
            }
        }
        else
        {
            // �÷��̾ ������ ��� ����
            _lastKnownPlayerPosition = enemy.player.position;
            _lastKnownPlayerPositionTimer = 0f;
            enemy.navMeshAgent.SetDestination(_lastKnownPlayerPosition);

            // ���� ���� ���� ���Դ��� Ȯ��
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distanceToPlayer <= enemy.attackRange)
            {
                enemy.TransitionToState(new AttackState());
                return;
            }
        }

        // �÷��̾ ���ƴ��� Ȯ��
        if (!CanSeePlayer(enemy) && _lastKnownPlayerPositionTimer > enemy.memoryDuration / 2)
        {
            enemy.TransitionToState(new SearchState());
            return;
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // �ʿ��� Ʈ���� �̺�Ʈ ó��
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

// ���� ����
public class AttackState : IEnemyState
{
    private float _attackTimer = 0f;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
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

        // �÷��̾ �ٶ󺸱�
        enemy.transform.LookAt(new Vector3(enemy.player.position.x, enemy.
       transform.position.y, enemy.player.position.z));
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= enemy.attackCooldown)
        {
            Attack(enemy);
            _attackTimer = 0f;
        }

        // ���� ���� ������ ������ �ٽ� ����
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distanceToPlayer > enemy.attackRange)
        {
            enemy.TransitionToState(new ChaseState());
        }
    }

    public void ExitState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
        enemy.navMeshAgent.isStopped = false;
    }

    public void OnTriggerState(EnemyStateController enemy, Collider other)
    {
        // �ʿ��� Ʈ���� �̺�Ʈ ó��
    }

    private void Attack(EnemyStateController enemy)
    {
        Debug.Log("���� �����մϴ�!");
        // ���⿡ ���� ���� ������ ����
        // ��: �ִϸ��̼� ���, ������ ó�� ��
    }
}

// ���� ����
public class SearchState : IEnemyState
{
    private float _searchTimer = 0f;
    private Vector3 _lastKnownPlayerPosition;

    public void EnterState(EnemyStateController enemy)
    {
        Debug.Log("���� ���� ����");
        _searchTimer = 0f;
        _lastKnownPlayerPosition = enemy.player != null ?
        enemy.player.position : enemy.transform.position;

        // ������ �÷��̾� ��ġ �ֺ��� ����
        enemy.navMeshAgent.SetDestination(_lastKnownPlayerPosition);
    }

    public void UpdateState(EnemyStateController enemy)
    {
        _searchTimer += Time.deltaTime;
        // �÷��̾ �ٽ� �߰��ߴ��� Ȯ��
        if (CanSeePlayer(enemy))
        {
            enemy.TransitionToState(new ChaseState());
            return;
        }

        // ���� �ð� ���� ã�� ���ϸ� ���� ���·� ��ȯ
        if (_searchTimer >= enemy.searchDuration)
        {
            enemy.TransitionToState(new PatrolState());
            return;
        }

        // �������� ���������� �÷��̾ �߰����� ���� ���, �ֺ��� �����ϰ� ����
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
        Debug.Log("���� ���� ����");
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

// �� ���� ��Ʈ�ѷ�
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

    // ���� ����
    private IEnemyState _currentState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
    }

    private void Start()
    {
        // �⺻ ���·� ���� ����
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

    // ���� ��ȯ �޼���
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