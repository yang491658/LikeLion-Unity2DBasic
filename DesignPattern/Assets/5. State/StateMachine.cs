public class StateMachine 
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit(); // 이전 상태의 Exit 실행
        currentState = newState; // 새로운 상태로 변경
        currentState.Enter(); // 새로운 상태의 Enter 실행
    }

    public void Update()
    {
        currentState?.Update(); // 현재 상태의 Update 실행
    }
}