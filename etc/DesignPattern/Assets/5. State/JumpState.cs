using UnityEngine;

public class JumpState : IState
{
    public void Enter() { Debug.Log("JUMP 상태 시작"); }
    public void Update() { Debug.Log("JUMP 상태 유지중"); }
    public void Exit() { Debug.Log("JUMP 상태 종료"); }
}
