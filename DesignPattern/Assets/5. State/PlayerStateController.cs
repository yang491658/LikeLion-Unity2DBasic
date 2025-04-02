using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState()); // 시작 시 IDLE 상태
    }

    void Update()
    {
        stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(new JumpState()); // 스페이스바 입력 시 JUMP 상태 변경

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            stateMachine.ChangeState(new RunState()); // 방향키 입력 시 RUN 상태 변경

        else if (!Input.anyKey)
            stateMachine.ChangeState(new IdleState()); // 키 입력 없을 시 IDLE 상태
    }
}