using UnityEngine;

public class Launcher : MonoBehaviour
{
    // 프리팹을 저장할 변수 : 총알
    public GameObject bullet;

    void Start()
    {
        // 반복 실행 함수 : InvokeRepeating(함수, 초기 지연, 반복 지연);
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    void Shoot()
    {
        // 생성 함수 : Instantiate(생성할 오브젝트, 생성 위치 벡터, 방향값) -> 총알, 런쳐와 동일, 없음
        Instantiate(bullet, transform.position, Quaternion.identity);

        // 발사 사운드 추가 (싱글톤)
        SoundManager.instance.PlayShootSound();
    }

    void Update()
    {
        
    }
}
