using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int combo; // �޺�
    private float comboDuration = 2; // �޺� ���ӽð�
    private float lastAttack; // ������ ����

    // ������ - ���
    public PlayerAttackState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (combo > 2 || // �ִ� �޺� ����
            Time.time >= lastAttack + comboDuration) // �޺� ���ӽð� ����
        {
            combo = 0; // �޺� �ʱ�ȭ
        }

        player.anim.SetInteger("Combo", combo); // �÷��̾� �ִϸ��̼� ����

        stateTimer = 0.1f; // ���� Ÿ�̸� �ʱ�ȭ = �޺� ���� ����

        // ���� ���� ����
        float attackDirection = player.direction;
        if (xInput != 0) attackDirection = xInput; // �¿� ���� Ű �Է�

        // �÷��̾� ���� �̵�
        player.SetVelocity(player.attackMovement[combo].x * attackDirection, player.attackMovement[combo].y);
    }

    public override void Update()
    {
        base.Update();

        if (trigger) // Ʈ���� �ߵ�
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }

        if (stateTimer < 0) // ���� ���� ����
        {
            // �÷��̾� ����
            //rb.linearVelocity = new Vector2(0, 0);
            player.SetZeroVelocity();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �÷��̾� �ൿ �ڷ�ƾ
        player.StartCoroutine("Act", 0.1f);

        combo++; // �޺� ���
        lastAttack = Time.time; // ������ ���� ����
    }
}