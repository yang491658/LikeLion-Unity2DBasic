using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPun, IPunObservable
{
    public float speed;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!photonView.AmOwner) return; // 로컬이 아닐 시 종료

        Launch();
    }

    // 발사 함수
    private void Launch()
    {
        if (!photonView.AmOwner) return; // 로컬이 아닐 시 종료

        // 랜덤 방향
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        // 이동
        rb.linearVelocity = new Vector2(x * speed, y * speed);
    }

    // 초기화 함수
    public void Reset()
    {
        // 정지 및 초기화
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        Invoke("Launch", 1); // 1초 후 발사
    }

    // 동기화 함수
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // 로컬의 데이터 발신
        {
            // 공의 위치가 속도 정보 발신
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
        }
        else // 원격의 데이터 수신
        {
            // 공의 위치가 속도 정보 수신 및 적용
            rb.position = (Vector2)stream.ReceiveNext();
            rb.linearVelocity = (Vector2)stream.ReceiveNext();
        }
    }
}