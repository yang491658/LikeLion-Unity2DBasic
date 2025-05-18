using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public bool isActing { get; private set; } // �ൿ ����

    [Header("�̵� ����")]
    public float moveSpeed = 12; // �̵� �ӵ�
    public float jumpForce; // ������
    private float moveSpeedSave; // �̵� �ӵ� ����
    private float jumpForceSave; // ������ ����

    [Header("�뽬 ����")]
    public float dashSpeed; // �뽬 �ӵ�
    private float dashSpeedSave; // �뽬 �ӵ� ����
    public float dashDirection { get; private set; } // �뽬 ����
    public float dashDuration; // �뽬 ���ӽð�
    [SerializeField] private float dashCooldown; // �뽬 ��ٿ�
    public float dashTimer; // �뽬 Ÿ�̸�

    [Header("���� ����")]
    public Vector2[] attackMovement; // ���� ������
    public float counterDuration = 0.2f; // �ݰ� ���ӽð�

    public SkillManager skill { get; private set; } // ��ų �Ŵ���
    public GameObject sword { get; private set; } // ���� ���� �ҵ�

    [Header("��ų ����")]
    public float swordReturnImpact; // �ҵ� ȸ�� �ݵ�

    [Header("�ڵ� ���� ����")]
    [SerializeField] protected LayerMask enemyLayer; // �� ���̾�
    public float attackDistance; // ���� �Ÿ�
    public float attackCooldown; // ���� ��ٿ�
    [HideInInspector] public float lastAttack; // ������ ����

    #region ����
    public PlayerStateMachine stateMachine { get; private set; } // �÷��̾� ���¸ӽ�

    // �÷��̾� ����
    public PlayerIdleState idleState { get; private set; } // �÷��̾� ��� ����
    public PlayerMoveState moveState { get; private set; } // �÷��̾� �̵� ����
    public PlayerAirState airState { get; private set; } // �÷��̾� ���� ����
    public PlayerJumpState jumpState { get; private set; } // �÷��̾� ���� ����
    public PlayerDashState dashState { get; private set; } // �÷��̾� �뽬 ����
    public PlayerWallSlideState wallSlideState { get; private set; } // �÷��̾� ��Ÿ�� ����
    public PlayerWallJumpState wallJumpState { get; private set; } // �÷��̾� ������ ����
    public PlayerAttackState attackState { get; private set; } // �÷��̾� ���� ����
    public PlayerCounterState counterState { get; private set; } // �÷��̾� �ݰ� ����
    public PlayerAimState aimState { get; private set; } // �÷��̾� ���� ����
    public PlayerCatchState catchState { get; private set; } // �÷��̾� ��� ����
    public PlayerBlackholeState blackholeState { get; private set; } // �÷��̾� ��Ȧ ����
    public PlayerDeadState deadState { get; private set; } // �÷��̾� ��� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���¸ӽ� �ν��Ͻ� ����
        stateMachine = new PlayerStateMachine();

        // �� ���� �ν��Ͻ� ����
        idleState = new PlayerIdleState(stateMachine, this, "Idle"); // �÷��̾� ��� ����
        moveState = new PlayerMoveState(stateMachine, this, "Move"); // �÷��̾� �̵� ����
        airState = new PlayerAirState(stateMachine, this, "Jump"); // �÷��̾� ���� ����
        jumpState = new PlayerJumpState(stateMachine, this, "Jump"); // �÷��̾� ���� ����
        dashState = new PlayerDashState(stateMachine, this, "Dash"); // �÷��̾� �뽬 ����
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide"); // �÷��̾� ��Ÿ�� ����
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump"); // �÷��̾� ������ ����
        attackState = new PlayerAttackState(stateMachine, this, "Attack"); // �÷��̾� ���� ����
        counterState = new PlayerCounterState(stateMachine, this, "Counter"); // �÷��̾� �ݰ� ����
        aimState = new PlayerAimState(stateMachine, this, "Aim"); // �÷��̾� ���� ����
        catchState = new PlayerCatchState(stateMachine, this, "Catch"); // �÷��̾� ��� ����
        blackholeState = new PlayerBlackholeState(stateMachine, this, "Jump"); // �÷��̾� ��Ȧ ����
        deadState = new PlayerDeadState(stateMachine, this, "Dead"); // �÷��̾� ��� ����
    }

    protected override void Start()
    {
        base.Start();

        // ���¸ӽ� �ʱ�ȭ = �÷��̾� ��� ����
        stateMachine.Initialize(idleState);

        // ��ų �Ŵ��� �ʱ�ȭ (�̱���)
        skill = SkillManager.instance;

        // �ӵ� ���� ����
        moveSpeedSave = moveSpeed;
        jumpForceSave = jumpForce;
        dashSpeedSave = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        Dash(); // �뽬

        if (Input.GetKeyDown(KeyCode.H)) // HŰ �Է�
        {
            skill.crystal.CanUseSkill(); // ũ����Ż ��ų ��� ����
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // ���� 1Ű �Է�
        {
            Inventory.instance.UseFlask(); // �κ��丮 �ö�ũ ���
        }
    }

    // ��ȭ �Լ� (���)
    public override void Slow(float _slowPercentage, float _slowDuration)
    {
        // ��ȭ ����
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        // ��ȭ ���ӽð� ���� �� ��ȭ ���
        Invoke("CancelSlow", _slowDuration);
    }

    // ��ȭ ��� �Լ� (���)
    protected override void CancelSlow()
    {
        base.CancelSlow();

        // �ӵ� ���󺹱�
        moveSpeed = moveSpeedSave;
        jumpForce = jumpForceSave;
        dashSpeed = dashSpeedSave;
    }

    // �ൿ �ڷ�ƾ
    public IEnumerator Act(float _seconds)
    {
        isActing = true; // �ൿ ��
        yield return new WaitForSeconds(_seconds);
        isActing = false; // �ൿ ����
    }

    // �뽬 �Լ�
    public void Dash()
    {
        if (IsWall()) return; // �÷��̾ �� ���� �� ����

        dashTimer -= Time.deltaTime; // �뽬 Ÿ�̸� ����

        //if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0) // ���� ����Ʈ �Է� + �뽬 ��ٿ� ����
        if (dashTimer < 0 || // �뽬 ��ٿ� ���� 
            Input.GetKeyDown(KeyCode.LeftShift) && // ���� ����Ʈ �Է�
            SkillManager.instance.dash.CanUseSkill()) // ��ų ��� ����
        {
            dashTimer = dashCooldown; // �뽬 Ÿ�̸� �ʱ�ȭ = �뽬 ��ٿ�

            // �¿� ����Ű �Է�
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0) // �Է� ����
                dashDirection = direction;

            stateMachine.Change(dashState); // �÷��̾� �뽬 ���·� ����
        }
    }

    // �ҵ� �Ҵ� �Լ�
    public void AssignSword(GameObject _sword) => sword = _sword;

    // �ҵ� ��� �Լ�
    public void CatchSwrod()
    {
        stateMachine.Change(catchState); // �÷��̾� ��� ���·� ����
        Destroy(sword); // �ҵ� ����
    }

    // ��� �Լ�
    public override void Die()
    {
        base.Die();

        stateMachine.Change(deadState); // �÷��̾� ��� ���·� ����
    }

    // �� ���� �Լ�
    public virtual RaycastHit2D IsEnemy()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, 50, enemyLayer);
    public virtual RaycastHit2D IsEnemyOpposite()
        => Physics2D.Raycast(wallCheck.position, Vector2.left * direction, 50, enemyLayer);

    // �ִϸ��̼� Ʈ���� �Լ�
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
}