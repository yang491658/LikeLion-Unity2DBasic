using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask playerLayer; // 플레이어 레이어
    public string animName { get; private set; } // 애니메이션

    #region 상태
    public EnemyStateMachine stateMachine { get; private set; } // 적 상태머신
    #endregion

    [Header("이동 정보")]
    public float idleTime; // 대기 시간
    public float moveSpeed; // 이동 속도
    private float moveSpeedSave; // 이동 속도 저장

    [Header("공격 정보")]
    public float battleTime; // 전투 시간
    public float battleDistance; // 전투 거리
    public float attackCoolDown; // 공격 쿨다운
    [HideInInspector] public float lastAttack; // 마지막 공격

    [Header("기절 정보")]
    public float stunDuration; // 기절 지속시간
    public Vector2 stunDirection; // 기절 방향

    [Header("반격 정보")]
    [SerializeField] protected GameObject counterTime; // 반격 시간 (이미지)
    protected bool canCounter; // 반격 가능 여부

    protected override void Awake()
    {
        base.Awake();

        // 상태머신 인스턴스 생성
        stateMachine = new EnemyStateMachine();

        moveSpeedSave = moveSpeed; // 이동 속도 저장
    }

    // 애니메이션 할당 함수
    public virtual void AssignAnim(string _animName)
    {
        animName = _animName;
    }

    // 시간 정지 함수
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen) // 시간 정지
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else // 시간 정지 해제
        {
            moveSpeed = moveSpeedSave;
            anim.speed = 1;
        }
    }

    // 시간 정지 코루틴
    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true); // 시간 정지
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false); // 시간 정지 해제
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    // 반격 타이밍 활성화 함수
    public virtual void OpenCounterTime()
    {
        canCounter = true; // 반격 가능
        counterTime.SetActive(true); // 반격 시간 활성화
    }

    // 반격 타이밍 비활성화 함수
    public virtual void CloseCounterTime()
    {
        canCounter = false; // 반격 불가능
        counterTime.SetActive(false); // 반격 시간 비활성화
    }

    // 반격 가능 함수
    public virtual bool CanCounter()
    {
        // 반격 가능
        if (canCounter)
        {
            CloseCounterTime(); // 반격 타이밍 비활성화
            return true;
        }

        // 반격 불가능
        return false;
    }

    // 기즈모 그리기 함수
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 플레이어
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + direction * battleDistance, transform.position.y));
    }

    // 플레이어 감지 함수
    public virtual RaycastHit2D IsPlayer()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, 50, playerLayer);

    // 애니메이션 트리거 함수
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
}