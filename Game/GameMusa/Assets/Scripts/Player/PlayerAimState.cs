using UnityEngine;

public class PlayerAimState : PlayerState
{
    // ������ - ���
    public PlayerAimState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.ActiveDots(true); // ���� ��� Ȱ��ȭ
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1)) // ���콺 ��Ŭ�� ����
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }

        // �÷��̾� ����
        player.SetZeroVelocity();

        // ���콺 ��ġ�� �÷��̾� ���� ȸ��
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.transform.position.x > mousePosition.x && player.direction == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.direction == -1)
            player.Flip();
    }

    public override void Exit()
    {
        base.Exit();

        // �÷��̾� �ൿ �ڷ�ƾ
        player.StartCoroutine("Act", 0.2f);
    }
}
