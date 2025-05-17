using UnityEngine;

public class RunState : IState
{
    public void Enter() { Debug.Log("RUN 상태 시작"); }
    public void Update() { Debug.Log("RUN 상태 유지중"); }
    public void Exit() { Debug.Log("RUN 상태 종료"); }
}