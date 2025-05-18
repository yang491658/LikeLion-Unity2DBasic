public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; } // 현재 상태

    // 상태 초기화 함수
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState; // 상태 초기화
        currentState.Enter(); // 상태 시작
    }

    // 상태 변경 함수
    public void Change(EnemyState _newState)
    {
        currentState.Exit(); // 상태 종료
        currentState = _newState; // 새로운 상태
        currentState.Enter(); // 상태 시작
    }
}