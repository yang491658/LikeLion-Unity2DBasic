using Photon.Pun;
using UnityEngine;

public class NetPaddle : MonoBehaviourPun
{
    public float speed = 10; // 속도

    void Update()
    {
        if (photonView.IsMine) // 객체가 로컬인지 여부
        {
            // 이동 = 수직 입력 방향 x 속도 x 시간
            float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            // 플레이어 상하 이동
            transform.Translate(0, move, 0);
        }
    }
}