using UnityEngine;

public class Player : MonoBehaviour
{
    public DynamicJoystick joystick; // 조이스틱
    public GameObject missile; // 미사일
    public bool isShooting = false; // 발사 여부

    private void Update()
    {
        // 방향 입력
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 direction = new Vector3(x, y, 0);

        // 플레이어 이동
        transform.Translate(direction * 3 * Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 입력
        //{
        //    // 미사일 생성
        //    Instantiate(missile, transform.position, Quaternion.identity);
        //}

        if (isShooting) // 발사 중
        {
            // 미사일 생성
            Instantiate(missile, transform.position, Quaternion.identity);
        }
    }

    // 발사 함수
    public void Shoot()
    {
        // 미사일 생성
        Instantiate(missile, transform.position, Quaternion.identity);
    }

    // 발사 시작 함수
    public void EnterShooting() => isShooting = true;

    // 발사 종료 함수
    public void ExitShooting() => isShooting = false;
}