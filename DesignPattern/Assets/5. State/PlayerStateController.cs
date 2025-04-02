using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState()); // ���� �� IDLE ����
    }

    void Update()
    {
        stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(new JumpState()); // �����̽��� �Է� �� JUMP ���� ����

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            stateMachine.ChangeState(new RunState()); // ����Ű �Է� �� RUN ���� ����

        else if (!Input.anyKey)
            stateMachine.ChangeState(new IdleState()); // Ű �Է� ���� �� IDLE ����
    }
}