using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet; // 미사일 프리팹 가져올 변수

    void Start()
    {
        // InvokeRepeating (함수이름, 초기지연시간, 지연할 시간)
        InvokeRepeating("Shoot", 0.3f, 0.3f);
    }

    void Shoot()
    {
        // Instantiate(미사일 프리팹, 런쳐포지션, 방향없음)
        Instantiate(bullet, transform.position, Quaternion.identity);

        // 사운드 사용 : 사운드 매니저에서 총알 사운드
        SoundManager.instance.PlayBulletSound();
    }

    void Update()
    {

    }
}
