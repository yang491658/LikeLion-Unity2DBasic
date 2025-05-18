using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine; // �÷��̾� ���¸ӽ�
    protected Player player; // �÷��̾�

    protected Rigidbody2D rb; // ������ٵ�

    private string animName; // �ִϸ��̼� �̸�
    protected float xInput; // �¿� ���� �Է°�
    protected float yInput; // ���� ���� �Է°�
    protected float stateTimer; // ���� Ÿ�̸�
    protected bool trigger; // Ʈ���� �ߵ� ����

    // ������
    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animName = _animName;
    }

    public virtual void Enter()
    {
        rb = player.rb; // �÷��̾��� ������ٵ�

        player.anim.SetBool(animName, true); // �÷��̾� �ִϸ��̼� ����

        trigger = false; // Ʈ���� �ʱ�ȭ
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // ���� Ÿ�̸� ����

        xInput = Input.GetAxisRaw("Horizontal"); // �¿� ����Ű �Է�
        yInput = Input.GetAxisRaw("Vertical"); // ���� ����Ű �Է�

        player.anim.SetFloat("ySpeed", rb.linearVelocityY); // �÷��̾� �ִϸ��̼� ����
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animName, false); // �÷��̾� �ִϸ��̼� ����
    }

    // �ִϸ��̼� Ʈ���� �Լ�
    public virtual void AnimationTrigger()
    {
        trigger = true; // Ʈ���� �ߵ�
    }
}