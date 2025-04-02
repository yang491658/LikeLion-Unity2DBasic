using UnityEngine;

public class IdleState : IState
{
    public void Enter() { Debug.Log("IDLE 상태 시작"); }
    public void Update() { Debug.Log("IDLE 상태 유지중"); }
    public void Exit() { Debug.Log("IDLE 상태 종료"); }
}