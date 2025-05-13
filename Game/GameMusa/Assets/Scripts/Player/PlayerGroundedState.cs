using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // ������ - ���
    public PlayerGroundedState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGround() && // �÷��̾ �ٴ� ����
            Input.GetKey(KeyCode.Space)) // �����̽� �Է� ����
        {
            stateMachine.Change(player.jumpState); // �÷��̾� ���� ���·� ����
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift)) // ���� ����Ʈ �Է�
        //{
        //    stateMachine.ChangeState(player.dashState); // �÷��̾� �뽬 ���·� ����
        //}

        if (!player.IsGround()) // �÷��̾ �ٴ� �̰���
        {
            stateMachine.Change(player.airState); // �÷��̾� ���� ���·� ����
        }

        if (player.IsGround() && // �÷��̾ �ٴ� ����
            Input.GetKeyDown(KeyCode.Mouse0)) // ���콺 ��Ŭ��
        {
            stateMachine.Change(player.attackState); // �÷��̾� ���� ���·� ����
        }

        if (Input.GetKeyDown(KeyCode.E)) // EŰ �Է�
        {
            stateMachine.Change(player.counterState); // �÷��̾� �ݰ� ���·� ����
        }

        //if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� ��
        //if (Input.GetMouseButtonDown(1) && !player.sword) // ���콺 ��Ŭ�� �� + �ҵ� ���� ��
        if (Input.GetMouseButtonDown(1) && // ���콺 ��Ŭ�� ��
            HasSword()) // �ҵ� ���� ��
        {
            stateMachine.Change(player.aimState); // �÷��̾� ���� ���·� ����
        }

        if (Input.GetKeyDown(KeyCode.G)) // GŰ �Է�
        {
            stateMachine.Change(player.blackholeState); // �÷��̾� ��Ȧ ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    // �ҵ� ���� �Լ�
    private bool HasSword()
    {
        // �ҵ� ����
        if (!player.sword) return true;

        // �ҵ� ����
        player.sword.GetComponent<SwordSkillController>().ReturnSword(); // �ҵ� ȸ��
        return false;
    }
}