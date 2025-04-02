using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance; // 싱글톤
    private CinemachineImpulseSource impulseSource; // 임펄스 소스 객체

    void Awake()
    {
        instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>(); // 임펄스 소스 컴포넌트
    }

    // 카메라 흔들기
    public void Shake()
    {
        if (impulseSource != null)
        {
            // 기본 설정으로 임펄스 생성
            impulseSource.GenerateImpulse();
        }
    }
}